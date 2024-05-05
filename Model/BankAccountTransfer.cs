using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using UniversityTuitionPaymentV2.Model.Enums;

namespace UniversityTuitionPaymentV2.Model
{
    [Table("BankAccountTransfer")]
    public class BankAccountTransfer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(11)]
        [Required]
        public required string SenderCode { get; set; }
        [StringLength(11)]
        [Required]
        public required string ReceiverCode { get; set; }
        [Required]
        [DefaultValue(0)]
        public double TransferAmount { get; set; }
        [Required]
        [DefaultValue(TransferStatus.Pending)]
        public TransferStatus Status { get; set; }
        

        public string? StatusMessage { get; set; }
    }
}
