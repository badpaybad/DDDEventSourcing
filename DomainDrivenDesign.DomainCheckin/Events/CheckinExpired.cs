using System;
using System.Collections.Generic;
using DomainDrivenDesign.Core.Events;
using DomainDrivenDesign.DomainCheckin.Entities;

namespace DomainDrivenDesign.DomainCheckin.Events
{
    public class CheckinExpired : IEvent
    {
        public CheckinExpired(int checkinId, List<EmployeeInfo> staffs)
        {
            throw new NotImplementedException();
        }

        public int Version { get; set; }
    }
}