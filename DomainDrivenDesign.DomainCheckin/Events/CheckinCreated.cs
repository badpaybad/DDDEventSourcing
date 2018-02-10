using System;
using DomainDrivenDesign.Core.Events;
using DomainDrivenDesign.DomainCheckin.Entities;

namespace DomainDrivenDesign.DomainCheckin.Events
{
    public class CheckinCreated : IEvent
    {
        public CheckinCreated(int checkinId, bool b, CheckinStatus status)
        {
            throw new NotImplementedException();
        }

        public int Version { get; set; }
    }
}