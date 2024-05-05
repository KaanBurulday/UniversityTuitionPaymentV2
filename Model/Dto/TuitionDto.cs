using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using UniversityTuitionPaymentV2.Model.Enums;
using UniversityTuitionPaymentV2.Model.Dto;

namespace UniversityTuitionPaymentV2.Model
{
    public class TuitionDto
    {
        public double TuitionTotal { get; set; }
        public required string StudentNo { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public TuitionStatus Status { get; set; }
        public int TermId { get; set; }
    }

    public class TuitionResultDto : APIResultDto
    {
        public int Id { get; set; }
    }
}
