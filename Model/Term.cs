using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityTuitionPaymentV2.Model
{
    [Table("Term")]
    public class Term
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int StartYear { get; set; }
        [Required]
        public int EndYear { get; set; }
    }
}
