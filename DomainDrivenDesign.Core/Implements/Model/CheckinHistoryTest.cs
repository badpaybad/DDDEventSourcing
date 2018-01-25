using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainDrivenDesign.Core.Implements.Model
{
    [Table("CheckinHistoryTest")]
    public class CheckinHistoryTest
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }
        public Guid CheckinId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}