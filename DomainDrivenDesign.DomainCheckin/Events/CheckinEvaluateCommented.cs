using System;
using DomainDrivenDesign.Core.Events;

namespace DomainDrivenDesign.DomainCheckin.Events
{
    public class CheckinEvaluateCommented : IEvent
    {
        public CheckinEvaluateCommented(int checkinId, string comment, int fromEvaluatorId, int toEmployeeId)
        {
            throw new NotImplementedException();
        }

        public int Version { get; set; }
    }
}