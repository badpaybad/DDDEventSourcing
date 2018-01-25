using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainDrivenDesign.Core.Implements.Model
{
    [Table("CheckinTest")]
    public class CheckinTest
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public int Status { get; set; }
        public int EmployeeId { get; set; }
    }
}
