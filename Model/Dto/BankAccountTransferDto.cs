using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using UniversityTuitionPaymentV2.Model.Enums;

namespace UniversityTuitionPaymentV2.Model.Dto
{
    public class BankAccountTransferDto
    {
        public required string SenderCode { get; set; }
        public required string ReceiverCode { get; set; }
        public double TransferAmount { get; set; }
        public TransferStatus Status { get; set; }
        public string? StatusMessage { get; set; }
    }

    public class BankAccountTransferResultDto : APIResultDto
    {
        public int Id { get; set; }
    }
}
