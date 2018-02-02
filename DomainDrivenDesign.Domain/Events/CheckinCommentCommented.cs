using System;
using DomainDrivenDesign.Core.Events;
using DomainDrivenDesign.Core.EventSourcingRepository;

namespace DomainDrivenDesign.Domain.Events
{
    public class CheckinCommentCommented : BaseEvent
    {
        public readonly string Comment;
        public readonly int UserId;
        public readonly DateTime CreatedOn;
        public readonly Guid CheckinId;

        public CheckinCommentCommented(Guid id, Guid checkinId, string comment, int userId, DateTime createdOn) : base(id)
        {
            Comment = comment;
            UserId = userId;
            CreatedOn = createdOn;
            CheckinId = checkinId;
        }
    }
}