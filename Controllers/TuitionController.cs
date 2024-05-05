using Microsoft.AspNetCore.Mvc;
using UniversityTuitionPaymentV2.Model;
using UniversityTuitionPaymentV2.Source.Services;

namespace UniversityTuitionPaymentV2.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "3-Generic Controller")]
    public class TuitionController : ControllerBase
    {
        private ITuitionService _tuitionService;

        public TuitionController(ITuitionService tuitionService)
        {
            _tuitionService = tuitionService;
        }

        [HttpGet]
        public List<TuitionDto> Get()
        {
            List<Tuition> datas = _tuitionService.Get().ToList();
            List<TuitionDto> ret = new List<TuitionDto>();
            datas.ForEach(data => ret.Add(createTuitionDto(data)));
            return ret;
        }

        [HttpGet("{id}")]
        public TuitionDto GetTuitionById(int id)
        {
            Tuition data = _tuitionService.Get(id);
            return createTuitionDto(data);
        }

        [HttpPost("InsertTuition")]
        public TuitionResultDto InsertTuition([FromBody] TuitionDto tuitionDto)
        {
            TuitionResultDto ret = new TuitionResultDto();
            if (!ModelState.IsValid)
            {
                ret.Status = "FAILURE";
                ret.Message = "Invalid Model";
                return ret;
            }
            try
            {
                int tuitionId = _tuitionService.Insert(createTuition(tuitionDto));
                if (tuitionId != -1)
                {
                    ret.Id = _tuitionService.Get(tuitionId).Id;
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
        public int UpdateTuition([FromBody] TuitionDto tuitionDto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("Delete")]
        public int DeleteAll()
        {
            return _tuitionService.Delete();
        }

        [HttpDelete("Delete/{id}")]
        public int Delete(int id)
        {
            return _tuitionService.Delete(id);
        }

        private TuitionDto createTuitionDto(Tuition tuition)
        {
            TuitionDto ret = new TuitionDto()
            {
                TuitionTotal = tuition.TuitionTotal,
                StudentNo = tuition.StudentNo,
                LastPaymentDate = tuition.LastPaymentDate,
                Status = tuition.Status,
                TermId = tuition.TermId
            };
            return ret;
        }

        private Tuition createTuition(TuitionDto tuitionDto)
        {
            Tuition ret = new Tuition()
            {
                TuitionTotal = tuitionDto.TuitionTotal,
                StudentNo = tuitionDto.StudentNo,
                LastPaymentDate = tuitionDto.LastPaymentDate,
                Status = tuitionDto.Status,
                TermId = tuitionDto.TermId
            };
            return ret;
        }
    }
}
