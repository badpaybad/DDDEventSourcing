using System;
using DomainDrivenDesign.Core.Events;

namespace DomainDrivenDesign.DomainCheckin.Events
{
    public class CheckinEvaluateCompleted : IEvent
    {
        public CheckinEvaluateCompleted(string checkinId, int fromEvaluatorId, int toEmployeeId)
        {
            throw new NotImplementedException();
        }

        public int Version { get; set; }
    }
}