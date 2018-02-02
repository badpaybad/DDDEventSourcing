using System;
using DomainDrivenDesign.Core.Events;
using DomainDrivenDesign.Core.EventSourcingRepository;

namespace DomainDrivenDesign.Domain.Events
{
    public class CheckinCreated : IEvent
    {
        public readonly Guid Id;
        public readonly DateTime StartDate;
        public readonly int Duration;
        public readonly int CreatedBy;
        public readonly DateTime CreatedDate;
        public readonly int Status;

        public CheckinCreated(Guid id, int status, DateTime startDate, int duration, int createdBy, DateTime createdDate)
        {
            Id = id;
            StartDate = startDate;
            Duration = duration;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            Status = status;
        }

        public int Version { get; set; }
    }
}