using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using UniversityTuitionPaymentV2.Model;
using UniversityTuitionPaymentV2.Model.Constants;
using UniversityTuitionPaymentV2.Model.Dto;
using UniversityTuitionPaymentV2.Model.Enums;
using UniversityTuitionPaymentV2.Source.Services;

namespace UniversityTuitionPaymentV2.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "1-Midterm Controller")]
    public class BankingApp : ControllerBase
    {
        private IBankAccountService _bankAccountService;
        private IBankAccountTransferService _bankAccountTransferService;
        private IStudentService _studentService;
        private ITuitionService _tuitionService;
        private ITermService _termService;
        private IUniversityService _universityService;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMessageSenderService _messageSenderService;

        public BankingApp(IBankAccountService bankAccountService,
                            IStudentService studentService,
                            ITuitionService tuitionService,
                            ITermService termService,
                            IBankAccountTransferService bankAccountTransferService,
                            IUniversityService universityService, 
                            ILogger<WeatherForecastController> logger,
                            IMessageSenderService messageSenderService)
        {
            _bankAccountService = bankAccountService;
            _studentService = studentService;
            _tuitionService = tuitionService;
            _termService = termService;
            _bankAccountTransferService = bankAccountTransferService;
            _universityService = universityService;
            _logger = logger;
            _messageSenderService = messageSenderService;
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
            if( (student != null) && (student.Status != StudentStatus.Passive))
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

        [HttpPost("PayTuition/{studentNo}/{start}-{end}")]
        public async Task<IActionResult> PayTuition(string studentNo, int start, int end)
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

                Tuition tuition = _tuitionService.GetTuitionByStudentNoAndTerm(studentNo, term);
                if (tuition == null)
                {
                    return NotFound("Tuition not found");
                }

                University university = _universityService.Get(student.CurrentUniversityId);
                if (university == null)
                {
                    return NotFound("University not found");
                }
                if (university.BankAccountId == null)
                {
                    return NotFound("University Bank Account not found");
                }

                BankAccount universityBankAccount = _bankAccountService.Get(university.BankAccountId.Value);
                if (universityBankAccount == null)
                {
                    return NotFound("University Bank Account not found");
                }

                BankAccount studentBankAccount = _bankAccountService.GetByTCKN(student.TCKimlikNo);
                if (studentBankAccount == null)
                {
                    return NotFound("Student Bank Account not found");
                }

                BankAccountTransfer transfer = _bankAccountTransferService.CreateBankAccountTransfer(
                    senderCode: studentBankAccount.AccountCode,
                    receiverCode: universityBankAccount.AccountCode,
                    amount: tuition.TuitionTotal);

                BankAccountTransfer transfered = _bankAccountTransferService.MakeTransfer(transfer);
                tuition.TuitionTotal = tuition.TuitionTotal - transfered.TransferAmount;
                _tuitionService.Update(tuition);


                student.Status = StudentStatus.Active;
                _studentService.Update(student);

                PaymentInfo paymentInfo = new PaymentInfo()
                {
                    BankAccountTransferId = transfered.Id,
                    StudentId = student.Id,
                    TuitionId = tuition.Id,
                };

                await _messageSenderService.SendMessageAsync(paymentInfo);

                return Ok(transfer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during tuition payment");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("{accountCode}/AddBalance/{amount}")]
        public IActionResult AddBalance(string accountCode, double amount)
        {
            BankAccount bankAccount = _bankAccountService.GetBankAccountByAccountCode(accountCode);
            if(bankAccount != null)
            {
                double balance = _bankAccountService.AddBalance(bankAccount, amount);
                var data = new APIResultDto { Status = "200", Message = "New Balance: " + balance };
                return new JsonResult(data);
            }
            else
            {
                var data = new { Status = "400", Message = "Bank Account Not Found." };
                return new JsonResult(data);
            }
        }

        [HttpPost("{accountCode}/SubtractBalance/{amount}")]
        public IActionResult SubtractBalance(string accountCode, double amount)
        {
            BankAccount bankAccount = _bankAccountService.GetBankAccountByAccountCode(accountCode);
            if (bankAccount != null)
            {
                double balance = _bankAccountService.SubtractBalance(bankAccount, amount);
                if (balance == -1)
                    return new JsonResult("Your operation has been denied");
                var data = new APIResultDto { Status = "200", Message = "New Balance: " + balance };
                return new JsonResult(data);
            }
            else
            {
                var data = new { Status = "400", Message = "Bank Account Not Found." };
                return new JsonResult(data);
            }
        }
    }
}
