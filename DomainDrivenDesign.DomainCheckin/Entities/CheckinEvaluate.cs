using System.Collections.Generic;

namespace DomainDrivenDesign.DomainCheckin.Entities
{
    public class CheckinEvaluate
    {
        public int EmployeeId { get; set; }
        public int EvaluatorId { get; set; }
        public CheckinCommentStatus Status { get; set; }

        public List<CheckinComment> Comments { get; set; }
    }
}