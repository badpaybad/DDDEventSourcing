using System;
using DomainDrivenDesign.Core.Events;
using DomainDrivenDesign.Core.EventSourcingRepository;

namespace DomainDrivenDesign.Domain.Events
{
    public class CheckinCreated : BaseEvent
    {
        public  DateTime StartDate { get; private set; }
        public  int Duration { get; private set; }
        public  int CreatedBy { get; private set; }
        public  DateTime CreatedDate { get; private set; }
        public  int Status { get; private set; }

        public CheckinCreated(Guid id, int status, DateTime startDate, int duration, int createdBy, DateTime createdDate):base(id)
        {
            StartDate = startDate;
            Duration = duration;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            Status = status;
        }

    }
}