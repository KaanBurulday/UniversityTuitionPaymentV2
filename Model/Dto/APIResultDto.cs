namespace UniversityTuitionPaymentV2.Model.Dto
{
    public class APIResultDto
    {
        public APIResultDto()
        {
            Status = "SUCCESS";
        }

        public string Status { get; set; }
        public string Message { get; set; }
    }
}
