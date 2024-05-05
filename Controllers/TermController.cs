using Microsoft.AspNetCore.Mvc;
using UniversityTuitionPaymentV2.Model;
using UniversityTuitionPaymentV2.Source.Services;

namespace UniversityTuitionPaymentV2.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "3-Generic Controller")]
    public class TermController : ControllerBase
    {
        private ITermService _termService;

        public TermController(ITermService termService)
        {
            _termService = termService;
        }

        [HttpGet]
        public List<TermDto> Get()
        {
            List<Term> datas = _termService.Get().ToList();
            List<TermDto> ret = new List<TermDto>();
            datas.ForEach(data => ret.Add(createTermDto(data)));
            return ret;
        }

        [HttpGet("{id}")]
        public TermDto GetTermById(int id)
        {
            Term data = _termService.Get(id);
            return createTermDto(data);
        }

        [HttpPost("InsertTerm")]
        public TermResultDto InsertTerm([FromBody] TermDto termDto)
        {
            TermResultDto ret = new TermResultDto();
            if (!ModelState.IsValid)
            {
                ret.Status = "FAILURE";
                ret.Message = "Invalid Model";
                return ret;
            }
            try
            {
                int termId = _termService.Insert(createTerm(termDto));
                if (termId != -1)
                {
                    ret.Id = _termService.Get(termId).Id;
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
        public int UpdateTerm([FromBody] TermDto termDto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("Delete")]
        public int DeleteAll()
        {
            return _termService.Delete();
        }

        [HttpDelete("Delete/{id}")]
        public int Delete(int id)
        {
            return _termService.Delete(id);
        }

        private TermDto createTermDto(Term term)
        {
            TermDto ret = new TermDto()
            {
                StartYear = term.StartYear,
                EndYear = term.EndYear
            };
            return ret;
        }

        private Term createTerm(TermDto termDto)
        {
            Term ret = new Term()
            {
                StartYear = termDto.StartYear,
                EndYear = termDto.EndYear
            };
            return ret;
        }
    }
}
