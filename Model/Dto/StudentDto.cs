using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using UniversityTuitionPaymentV2.Model.Enums;
using UniversityTuitionPaymentV2.Model.Dto;

namespace UniversityTuitionPaymentV2.Model
{
    public class StudentDto
    {
        public required string StudentNo { get; set; }
        public required string StudentName { get; set; }
        public required string TCKimlikNo { get; set; }
        public required int CurrentUniversityId { get; set; }
        public StudentStatus Status { get; set; }
    }

    public class StudentResultDto : APIResultDto
    {
        public int Id { get; set; }
    }
}
