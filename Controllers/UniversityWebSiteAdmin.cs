using Microsoft.AspNetCore.Mvc;
using UniversityTuitionPaymentV2.Model;
using UniversityTuitionPaymentV2.Model.Enums;
using UniversityTuitionPaymentV2.Source.Services;

namespace UniversityTuitionPaymentV2.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "1-Midterm Controller")]
    public class UniversityWebSiteAdmin : ControllerBase
    {
        private IBankAccountService _bankAccountService;
        private IBankAccountTransferService _bankAccountTransferService;
        private IStudentService _studentService;
        private ITuitionService _tuitionService;
        private ITermService _termService;
        private IUniversityService _universityService;
        private readonly ILogger<WeatherForecastController> _logger;

        public UniversityWebSiteAdmin(IBankAccountService bankAccountService,
                            IStudentService studentService,
                            ITuitionService tuitionService,
                            ITermService termService,
                            IBankAccountTransferService bankAccountTransferService,
                            IUniversityService universityService,
                            ILogger<WeatherForecastController> logger)
        {
            _bankAccountService = bankAccountService;
            _studentService = studentService;
            _tuitionService = tuitionService;
            _termService = termService;
            _bankAccountTransferService = bankAccountTransferService;
            _universityService = universityService;
            _logger = logger;
        }

        [HttpPost("AddTuition/{studentNo}/{start}-{end}")]
        public IActionResult AddTuition(string studentNo, int start, int end)
        {
            try
            {
                if (string.IsNullOrEmpty(studentNo) || start <= 0 || end <= 0)
                {
                    return BadRequest("Invalid parameters");
                }

                Student student = _studentService.Get(studentNo);
                if (student == null)
                {
                    return NotFound("Student not found");
                }

                if (student.Status == StudentStatus.Passive)
                {
                    return BadRequest("Student is passive");
                }

                Term term = _termService.GetByYears(start, end);
                if (term == null)
                {
                    return NotFound("Term not found");
                }

                University university = _universityService.Get(student.CurrentUniversityId);
                if (university == null)
                {
                    return NotFound("University not found");
                }

                Tuition tuition = _tuitionService.GetTuitionByStudentNoAndTerm(studentNo, term);
                if(tuition != null)
                {
                    return Conflict("Tuition already exists");
                }
                
                tuition = new Tuition()
                {
                    TuitionTotal = university.CurrentTuitionFee,
                    StudentNo = student.StudentNo,
                    LastPaymentDate = DateTime.Now.AddDays(14),
                    Status = TuitionStatus.Pending,
                    TermId = term.Id
                };
                _tuitionService.Insert(tuition);

                student.Status = StudentStatus.PaymentPending;
                _studentService.Update(student);

                return Ok(createTuitionDto(tuition));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during tuition payment");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("UnpaidTuitionStatus")]
        public List<StudentDto> GetUnpaidTuitionStatus()
        {
            List<Student> datas = _studentService.Get().ToList();
            List<StudentDto> ret = new List<StudentDto>();
            datas.ForEach(data => { if(data.Status == StudentStatus.PaymentPending) ret.Add(createStudentDto(data)); });
            return ret;
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

        private StudentDto createStudentDto(Student student)
        {
            StudentDto ret = new StudentDto()
            {
                StudentNo = student.StudentNo,
                TCKimlikNo = student.TCKimlikNo,
                StudentName = student.StudentName,
                CurrentUniversityId = student.CurrentUniversityId,
                Status = student.Status
            };
            return ret;
        }
    }
}
