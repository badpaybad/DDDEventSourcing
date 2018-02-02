using System;
using DomainDrivenDesign.Core.Events;
using DomainDrivenDesign.Core.EventSourcingRepository;

namespace DomainDrivenDesign.Domain.Events
{
    public class CheckinCompleted : IEvent
    {
        public readonly Guid Id;
        public readonly int Status;
        public CheckinCompleted(Guid id, int status) 
        {
            Id = id;
            Status = status;
        }

        public int Version { get; set; }
    }
}