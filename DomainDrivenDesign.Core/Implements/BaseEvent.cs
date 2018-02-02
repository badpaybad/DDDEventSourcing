using System;
using DomainDrivenDesign.Core.Events;

namespace DomainDrivenDesign.Core.Implements
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