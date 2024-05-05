using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UniversityTuitionPaymentV2.Model.Enums;

namespace UniversityTuitionPaymentV2.Model
{
    [Table("Tuition")]
    public class Tuition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [DefaultValue(-1)]
        public double TuitionTotal { get; set; }
        [Required]
        [StringLength(11)]
        public required string StudentNo { get; set; }
        [Required]
        public DateTime LastPaymentDate { get; set; }
        [Required]
        [DefaultValue(TuitionStatus.Pending)]
        public TuitionStatus Status { get; set; }
        [Required]
        public int TermId { get; set; }

        public Term? Term { get; set; }
    }
}
