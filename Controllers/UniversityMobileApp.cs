using Microsoft.AspNetCore.Mvc;
using UniversityTuitionPaymentV2.Model.Enums;
using UniversityTuitionPaymentV2.Model;
using UniversityTuitionPaymentV2.Source.Services;

namespace UniversityTuitionPaymentV2.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "1-Midterm Controller")]
    public class UniversityMobileApp : ControllerBase
    {
        private IBankAccountService _bankAccountService;
        private IStudentService _studentService;
        private ITuitionService _tuitionService;
        private readonly ILogger<WeatherForecastController> _logger;

        public UniversityMobileApp(IBankAccountService bankAccountService,
                            IStudentService studentService,
                            ITuitionService tuitionService,
                            ILogger<WeatherForecastController> logger)
        {
            _bankAccountService = bankAccountService;
            _studentService = studentService;
            _tuitionService = tuitionService;
            _logger = logger;
        }

        [HttpGet("QueryTuition/{studentNo}")]
        public IActionResult QueryTuition(string studentNo)
        {
            /*
             * 1. Get the student by studentNo from student service
             * 2. Get the bank account by the student's TCKimlikNo from bank account service
             * 3. Get the tuition by studentNo from tuition service
             */
            Student student = _studentService.Get(studentNo);
            if ((student != null) && (student.Status != StudentStatus.Passive))
            {
                BankAccount bankAccount = _bankAccountService.GetByTCKN(student.TCKimlikNo);
                Tuition tuition = _tuitionService.Get(studentNo);
                var data = new { TuitionTotal = tuition.TuitionTotal, Balance = bankAccount.Balance };
                return Ok(data);
            }
            else
            {
                return NotFound("Student not found or is passvive");
            }
        }
    }
}
