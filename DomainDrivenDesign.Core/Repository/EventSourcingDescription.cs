using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainDrivenDesign.Core.Repository
{
    [Table("EventSourcingDescription")]
    internal class EventSourcingDescription
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(512)]
        public string Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public int Version { get; set; }

        [StringLength(512)]
        public string AggregateType { get; set; }

        [StringLength(512)]
        public string EventType { get; set; }

        public string EventData { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}