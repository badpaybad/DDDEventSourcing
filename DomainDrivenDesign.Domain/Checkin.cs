﻿using System;
using System.Collections.Generic;
using DomainDrivenDesign.Core;
using DomainDrivenDesign.Core.Events;
using DomainDrivenDesign.Domain.Entities;
using DomainDrivenDesign.Domain.Events;

namespace DomainDrivenDesign.Domain
{
    public class Checkin : AggregateRoot
    {

        public enum CheckinStatus
        {
            Pending,
            Completed,
            Expired
        }

        public Checkin()
        {
        }
        
        public override string Id { get; set; }

        private CheckinStatus Status;

        private int Duration;

        private DateTime StartDate;

        List<CheckinComment> Comments = new List<CheckinComment>();

        void Apply(CheckinCreated e)
        {
            Id = e.Id.ToString();
            StartDate = e.StartDate;
            Duration = e.Duration;
            Status = CheckinStatus.Pending;
        }

        /// <summary>
        /// these kind of function will call when build up from list events to this Domain object
        /// check funtion LoadFromHistory in class AggregateRoot
        /// </summary>
        /// <param name="e"></param>
        void Apply(CheckinCompleted e)
        {
            Status = CheckinStatus.Completed;
        }

        void Apply(CheckinCommentCommented e)
        {
            Comments.Add(new CheckinComment() { Comment = e.Comment, CreatedOn = e.CreatedOn, UserId = e.UserId });
        }

        public Checkin(Guid id, DateTime startDate, int duration, int createdBy)
        {
            DateTime createdDate = DateTime.Now;
            ApplyChange(new CheckinCreated(id, (int)CheckinStatus.Pending, startDate, duration, createdBy, createdDate));
        }

        public void EmployeeComment(string comment, int employeeId)
        {
            if (Status == CheckinStatus.Expired) throw new Exception("Expired checkin");

            ApplyChange(new CheckinCommentCommented(Guid.NewGuid(),Guid.Parse(Id), comment, employeeId, createdOn: DateTime.Now));
        }

        public void EvaluatorComment(string comment, int evaluatorId)
        {
            if (Status == CheckinStatus.Expired) throw new Exception("Expired checkin");

            if (string.IsNullOrEmpty(comment)) throw new Exception("Comment empty");
            
            ApplyChange(new CheckinCommentCommented(Guid.NewGuid(), Guid.Parse(Id), comment, evaluatorId, createdOn: DateTime.Now));

            if (Status == CheckinStatus.Pending)
                ApplyChange(new CheckinCompleted(Guid.Parse(Id), (int)CheckinStatus.Completed));
        }

    }

}
