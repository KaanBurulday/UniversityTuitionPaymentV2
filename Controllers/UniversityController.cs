using Microsoft.AspNetCore.Mvc;
using UniversityTuitionPaymentV2.Model;
using UniversityTuitionPaymentV2.Source.Services;

namespace UniversityTuitionPaymentV2.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "3-Generic Controller")]
    public class UniversityController : ControllerBase
    {
        private IUniversityService _universityService;

        public UniversityController(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        [HttpGet]
        public List<UniversityDto> Get()
        {
            List<University> datas = _universityService.Get().ToList();
            List<UniversityDto> ret = new List<UniversityDto>();
            datas.ForEach(data => ret.Add(createUniversityDto(data)));
            return ret;
        }

        [HttpGet("{id}")]
        public UniversityDto GetUniversityById(int id)
        {
            University data = _universityService.Get(id);
            return createUniversityDto(data);
        }

        [HttpGet("{id}/StudentsEnrolled")]
        public List<Student> GetStudents(int id)
        {
            return _universityService.StudentsEnrolled(id);
        }

        [HttpPost("InsertUniversity")]
        public UniversityResultDto InsertUniversity([FromBody] UniversityDto university)
        {
            UniversityResultDto ret = new UniversityResultDto();
            if (!ModelState.IsValid)
            {
                ret.Status = "FAILURE";
                ret.Message = "Invalid Model";
                return ret;
            }
            try
            {
                int universityId = _universityService.Insert(createUniversity(university));
                if (universityId != -1)
                {
                    ret.Id = _universityService.Get(universityId).Id;
                }
            }
            catch (Exception ex)
            {
                ret.Status = "FAILURE";
                ret.Message = ex.Message;
            }
            return ret;
        }

        [HttpPut("Put")]
        public int UpdateUniversity([FromBody] UniversityDto university)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("Delete")]
        public int DeleteAll()
        {
            return _universityService.Delete();
        }

        [HttpDelete("Delete/{id}")]
        public int Delete(int id)
        {
            return _universityService.Delete(id);
        }

        private UniversityDto createUniversityDto(University university)
        {
            UniversityDto ret = new UniversityDto()
            {
                UniversityCode = university.UniversityCode,
                UniversityName = university.UniversityName,
                CurrentTuitionFee = university.CurrentTuitionFee,
                CurrentTermId = university.CurrentTermId,
                BankAccountId = university.BankAccountId,
                Students = university.Students
            };
            return ret;
        }

        private University createUniversity(UniversityDto universityDto)
        {
            University ret = new University()
            {
                UniversityCode = universityDto.UniversityCode,
                UniversityName = universityDto.UniversityName,
                CurrentTuitionFee = universityDto.CurrentTuitionFee,
                Students = universityDto.Students,
                CurrentTermId = universityDto.CurrentTermId,
                BankAccountId = universityDto.BankAccountId
            };
            return ret;
        }
    }
}
