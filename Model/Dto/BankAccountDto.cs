using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using UniversityTuitionPaymentV2.Model.Dto;
using UniversityTuitionPaymentV2.Model.Enums;

namespace UniversityTuitionPaymentV2.Model
{
    public class BankAccountDto
    {
        public required string AccountCode { get; set; }
        public double Balance { get; set; }
        public required string TCKimlikNo { get; set; }
        public BankAccountType AccountType { get; set; }
    }

    public class BankAccountResultDto : APIResultDto
    {
        public int Id { get; set; }
    }
}
