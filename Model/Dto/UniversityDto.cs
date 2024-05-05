using System.ComponentModel.DataAnnotations;
using UniversityTuitionPaymentV2.Model.Dto;

namespace UniversityTuitionPaymentV2.Model
{
    public class UniversityDto
    {
        public required string UniversityCode { get; set; }
        public required string UniversityName { get; set; }
        public required double CurrentTuitionFee { get; set; }
        public required int CurrentTermId { get; set; }
        public int? BankAccountId { get; set; }
        public ICollection<Student>? Students { get; set; }
    }

    public class UniversityResultDto : APIResultDto
    {
        public int Id { get; set; }
    }
}
