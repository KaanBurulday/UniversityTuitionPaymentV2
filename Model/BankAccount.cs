using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UniversityTuitionPaymentV2.Model.Enums;

namespace UniversityTuitionPaymentV2.Model
{
    [Table("BankAccount")]
    public class BankAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(11)]
        [Required]
        public required string AccountCode { get; set; }
        [Required]
        [DefaultValue(0)]
        public double Balance { get; set; }
        [StringLength(11)]
        public string? TCKimlikNo { get; set; }
        [Required]
        public required BankAccountType AccountType { get; set; }
    }
}
