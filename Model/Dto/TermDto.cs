using System.ComponentModel.DataAnnotations;
using UniversityTuitionPaymentV2.Model.Dto;

namespace UniversityTuitionPaymentV2.Model
{
    public class TermDto
    {
        public int StartYear { get; set; }
        public int EndYear { get; set; }
    }

    public class TermResultDto : APIResultDto
    {
        public int Id { get; set; }
    }
}
