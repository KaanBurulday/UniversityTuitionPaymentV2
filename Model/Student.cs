using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UniversityTuitionPaymentV2.Model.Enums;

namespace UniversityTuitionPaymentV2.Model
{
    [Table("Student")]
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(11)]
        public string StudentNo { get; set; }
        [Required]
        [MinLength(2)]
        public required string StudentName { get; set; }
        [Required]
        [StringLength(11)]
        public required string TCKimlikNo { get; set; }
        [Required]
        public required int CurrentUniversityId { get; set; }
        [Required]
        [DefaultValue(StudentStatus.Active)]
        public StudentStatus Status { get; set; }

        public University? CurrentUniversity { get; set; }
    }
}
