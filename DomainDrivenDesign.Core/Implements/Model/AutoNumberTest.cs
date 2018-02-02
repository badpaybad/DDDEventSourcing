using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainDrivenDesign.Core.Implements.Model
{
    [Table("AutoNumberTest")]
    public class AutoNumberTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}