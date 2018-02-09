using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenDesign.Core.Events;
using DomainDrivenDesign.DomainCheckin.Entities;

namespace DomainDrivenDesign.DomainCheckin.Events
{
    public class CheckinStarted : IEvent
    {
        public CheckinStarted(string checkinId, List<EmployeeInfo> staffs)
        {
            throw new NotImplementedException();
        }

        public int Version { get; set; }
    }
}
