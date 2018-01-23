using DomainDrivenDesign.Core.Commands;
using DomainDrivenDesign.Core.Repository;

namespace DomainDrivenDesign.Domain.Commands
{
    public class CheckinCommandHandle : ICommandHandle<CreateCheckin>, ICommandHandle<CommentCheckinByEmployee>, ICommandHandle<CommentCheckinByEvaluator>
    {
        private ICqrsEventSourcingRepository<Checkin> _repository;

        public CheckinCommandHandle()
        {
            _repository = new CqrsEventSourcingRepository<Checkin>(new Core.Hris.EventPublisher());
        }

        public void Handle(CreateCheckin c)
        {
            _repository.CreateNew(new Checkin(c.CheckinId,c.StartDate,c.Duration,c.CreatedBy));
        }

        public void Handle(CommentCheckinByEmployee c)
        {
            _repository.GetDoSave(c.CheckinId, a=>a.EmployeeComment(c.Comment,c.CreatedBy));
        }

        public void Handle(CommentCheckinByEvaluator c)
        {
            _repository.GetDoSave(c.CheckinId,a=>a.EvaluatorComment(c.Comment,c.CreatedBy));
        }
    }
}
