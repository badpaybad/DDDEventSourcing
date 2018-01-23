using System;
using DomainDrivenDesign.Core.Events;

namespace DomainDrivenDesign.Domain.Events
{
    public class CheckinCommentCommented : BaseEvent
    {
        public readonly string Comment;
        public readonly int UserId;
        public readonly DateTime CreatedOn;

        public CheckinCommentCommented(Guid id, string comment, int userId, DateTime createdOn) : base(id)
        {
            Comment = comment;
            UserId = userId;
            CreatedOn = createdOn;
        }
    }
}