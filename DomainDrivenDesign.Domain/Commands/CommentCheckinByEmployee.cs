using System;
using DomainDrivenDesign.Core.Commands;

namespace DomainDrivenDesign.Domain.Commands
{
    public class CommentCheckinByEmployee : ICommand
    {
        public CommentCheckinByEmployee(Guid checkinId, string comment, int createdBy)
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