using System;
using DomainDrivenDesign.Core.Events;
using DomainDrivenDesign.Core.EventSourcingRepository;
using DomainDrivenDesign.Core.Implements;

namespace DomainDrivenDesign.Domain.Events
{
    public class CheckinCommentCommented : IEvent
    {
        public readonly Guid Id;
        public readonly string Comment;
        public readonly int UserId;
        public readonly DateTime CreatedOn;
        public readonly Guid CheckinId;

        public CheckinCommentCommented(Guid id, Guid checkinId, string comment, int userId, DateTime createdOn)
        {
            Id = id;
            Comment = comment;
            UserId = userId;
            CreatedOn = createdOn;
            CheckinId = checkinId;
        }

        public int Version { get; set; }
    }
}