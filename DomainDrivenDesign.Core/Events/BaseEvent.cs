using System;

namespace DomainDrivenDesign.Core.Events
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