using System;
using DomainDrivenDesign.Core.Commands;

namespace DomainDrivenDesign.Domain.Commands
{
    public class CommentCheckinByEvaluator : ICommand
    {
        public CommentCheckinByEvaluator(Guid checkinId, string comment, int createdBy)
        {
            CheckinId = checkinId;
            Comment = comment;
            CreatedBy = createdBy;
        }

        public readonly Guid CheckinId;
        public readonly string Comment;
        public readonly int CreatedBy;
    }
}