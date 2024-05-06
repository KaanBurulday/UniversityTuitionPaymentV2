using System.ComponentModel.DataAnnotations;

namespace UniversityTuitionPaymentV2.Model.Constants
{
    public class PaymentInfo
    {
        [Required]
        public required int? BankAccountTransferId { get; set; }
        [Required]
        public required int? StudentId { get; set; }
        [Required]
        public required int? TuitionId { get; set; }

        public BankAccountTransfer? BankAccountTransfer { get; set; }
        public Student? Student { get; set; }
        public Tuition? Tuition { get; set; }
    }
}
