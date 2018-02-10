using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenDesign.Core;
using DomainDrivenDesign.Core.Events;
using DomainDrivenDesign.DomainCheckin.Entities;
using DomainDrivenDesign.DomainCheckin.Events;
using DomainDrivenDesign.DomainCheckin.Services;

namespace DomainDrivenDesign.DomainCheckin
{

    public class Checkin : AggregateRoot
    {
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;
        public override string Id { get; set; }

        private IEmployeeServices _employeeServices;
        private CheckinStatus _status;

        private List<CheckinEvaluate> _checkinEvaluates = new List<CheckinEvaluate>();

        public Checkin()
        {
        }

        public Checkin(int id, DateTime startDate, DateTime endDate)
        {
            Id = id.ToString();
            _startDate = startDate;
            _endDate = endDate;
            _status = CheckinStatus.New;

            ApplyChange(new CheckinCreated(int.Parse(Id), startDate< endDate,_status));
        }

        public void Start()
        {
            if (_startDate != DateTime.Now) throw new Exception("not valid date to start");

            if (_status == CheckinStatus.Expired)
            {
                throw new Exception("Expired");
            }

            if (_status != CheckinStatus.New)
            {
                throw new Exception("Already started");
            }

            var staffs = _employeeServices.FindStaffWithValidEffectiveDate(_startDate, _endDate);

            _status = CheckinStatus.Started;

            foreach (var staff in staffs)
            {
                _checkinEvaluates.Add(new CheckinEvaluate()
                {
                    Status = CheckinCommentStatus.Pending,
                    EmployeeId = staff.EmployeeId,
                    EvaluatorId = staff.EvaluatorId
                });
            }
            //can build email to send
            //or just email domain subscribe CheckinStarted to remind
            ApplyChange(new CheckinStarted(int.Parse(Id), staffs));
        }

        public void EvaluatorComment(string comment, int fromEvaluatorId, int toEmployeeId)
        {

            if (_status == CheckinStatus.Expired)
            {
                throw new Exception("Expired");
            }
            if (_status != CheckinStatus.Started)
            {
                throw new Exception("Not started");
            }

            var evl = _checkinEvaluates.FirstOrDefault(
                i => i.EvaluatorId == fromEvaluatorId && i.EmployeeId == toEmployeeId);
            if (evl == null) throw new Exception("No exist");

            if (evl.Status == CheckinCommentStatus.Pending)
            {
                evl.Status = CheckinCommentStatus.Completed;

                ApplyChange(new CheckinEvaluateCompleted(int.Parse(Id), fromEvaluatorId, toEmployeeId));
            }

            evl.Comments.Add(new CheckinComment() { Comment = comment, EmployeeId = fromEvaluatorId });

            ApplyChange(new CheckinEvaluateCommented(int.Parse(Id), comment, fromEvaluatorId, toEmployeeId));
        }

        public void EmployeeComment(string comment, int fromEmployeeId, int toEvaluatorId)
        {
            if (_status == CheckinStatus.Expired)
            {
                throw new Exception("Expired");
            }
            if (_status != CheckinStatus.Started)
            {
                throw new Exception("Not started");
            }

            var evl = _checkinEvaluates.FirstOrDefault(
                i => i.EvaluatorId == toEvaluatorId && i.EmployeeId == fromEmployeeId);

            if (evl == null) throw new Exception("No exist");

            evl.Comments.Add(new CheckinComment() { Comment = comment, EmployeeId = fromEmployeeId });

            ApplyChange(new CheckinEvaluateCommented(int.Parse(Id), comment, fromEmployeeId, toEvaluatorId));
        }

        public void NotifyBeforeExpire()
        {

            if (_status == CheckinStatus.Expired)
            {
                throw new Exception("Expired");
            }
            if (_status != CheckinStatus.Started)
            {
                throw new Exception("Not started");
            }

            if (DateTime.Now.AddDays(1) != _endDate) return;

            List<EmployeeInfo> staffs = _employeeServices.FindStaffWithValidEffectiveDate(_startDate, _endDate);
            //can build email to send
            //or just email domain subscribe CheckinRemidedBeforeExpire to remind
            ApplyChange(new CheckinRemidedBeforeExpire(int.Parse(Id), staffs));
        }

        public void Expired()
        {
            if (_status != CheckinStatus.Started)
            {
                throw new Exception("Not started");
            }

            _status = CheckinStatus.Expired;

            List<EmployeeInfo> staffs = _employeeServices.FindStaffWithValidEffectiveDate(_startDate, _endDate);

            ApplyChange(new CheckinExpired(int.Parse(Id), staffs));
        }
    }

    
}
