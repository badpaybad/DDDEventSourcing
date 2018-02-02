using System;
using DomainDrivenDesign.Core.Events;

namespace DomainDrivenDesign.Core.EventSourcingRepository
{
    public class BaseEvent : IEvent
    {
        public BaseEvent(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
        public int Version { get; set; }
    }
}