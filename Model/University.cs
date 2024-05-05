using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityTuitionPaymentV2.Model
{
    [Table("University")]
    public class University
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(5)]
        public required string UniversityCode { get; set; }
        [Required]
        [MaxLength(50)] 
        public required string UniversityName { get; set; }
        [Required]
        public double CurrentTuitionFee { get; set; }
        [Required]
        public required int CurrentTermId { get; set; }
        public int? BankAccountId { get; set; }
        public ICollection<Student>? Students { get; set; }

        public Term? Term { get; set; }
        public BankAccount? BankAccount { get; set; }
    }
}
