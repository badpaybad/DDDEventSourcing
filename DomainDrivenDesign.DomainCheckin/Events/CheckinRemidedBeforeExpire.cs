using System;
using System.Collections.Generic;
using DomainDrivenDesign.Core.Events;
using DomainDrivenDesign.DomainCheckin.Entities;

namespace DomainDrivenDesign.DomainCheckin.Events
{
    public class CheckinRemidedBeforeExpire : IEvent
    {
        public CheckinRemidedBeforeExpire(string checkinId, List<EmployeeInfo> staffs)
        {
            throw new NotImplementedException();
        }

        public int Version { get; set; }
    }
}