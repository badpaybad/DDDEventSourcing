using System;
using DomainDrivenDesign.Core.Events;

namespace DomainDrivenDesign.Domain.Events
{
    public class CheckinCompleted : BaseEvent
    {
        public readonly int Status;
        public CheckinCompleted(Guid id, int status) : base(id)
        {
            Status = status;
        }
    }
}