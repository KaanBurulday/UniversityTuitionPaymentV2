using Microsoft.AspNetCore.Mvc;
using UniversityTuitionPaymentV2.Model;
using UniversityTuitionPaymentV2.Model.Dto;
using UniversityTuitionPaymentV2.Source.Services;

namespace UniversityTuitionPaymentV2.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "2-Generator Controller")]
    public class GeneratorController : ControllerBase
    {
        private IBankAccountService _bankAccountService;
        private IStudentService _studentService;
        private ITermService _termService;
        private ITuitionService _tuitionService;
        private IUniversityService _universityService;

        private int _termCount { get; set; } = 1;
        private int _universityCount { get; set; } = 1;
        private int _studentCount { get; set; } = 25;

        public GeneratorController(IBankAccountService bankAccountService,
                                    IStudentService studentService,
                                    ITermService termService,
                                    ITuitionService tuitionService,
                                    IUniversityService universityService)
        {
            _bankAccountService = bankAccountService;
            _studentService = studentService;
            _termService = termService;
            _tuitionService = tuitionService;
            _universityService = universityService;
        }

        [HttpPost("GenerateEnv")]
        public APIResultDto GenerateEnv(int termCount, int universityCount, int studentCount)
        {
            APIResultDto result = new APIResultDto();
            try
            {
                if (termCount == 0)
                    termCount = _termCount;
                if (universityCount == 0)
                    universityCount = _universityCount;
                if (studentCount == 0)
                    studentCount = _studentCount;

                result.Message += CleanDB();

                result.Message += GenerateTerms(DateTime.Now.Year - termCount);

                result.Message += GenerateUniversities(universityCount);

                result.Message += GenerateUniversityBankAccounts();

                result.Message += GenerateStudents(studentCount);

                result.Message += GenerateStudentBankAccounts();

                result.Message += GenerateTuitions();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message + ' ' + ex.StackTrace;
                result.Status = "400";
            }
            return result;
        }

        [HttpPost("FullCleanDB")]
        public APIResultDto FullCleanDB()
        {
            APIResultDto result = new APIResultDto();
            try
            {
                result.Message += CleanDB();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message + ' ' + ex.StackTrace;
                result.Status = "400";
            }
            return result;
        }


        private string CleanDB()
        {
            int termDelete = _termService.Delete();
            int tuitionDelete = _tuitionService.Delete();
            int studentDelete = _studentService.Delete();
            int universityDelete = _universityService.Delete();
            int bankAccountDelete = _bankAccountService.Delete();
            return "Database Cleaned.";
        }

        private string GenerateTerms(int limit)
        {
            int CurrentYear = DateTime.Now.Year;
            int count = 0;
            for (int i = CurrentYear; i > limit; i--)
            {
                Term term = new Term()
                {
                    StartYear = i-1,
                    EndYear = i
                };
                _termService.Insert(term);
                count++;
            }
            return " Generated Terms: " + count;
        }

        private string GenerateUniversities(int limit)
        {
            for (int i = 0; i < limit; i++)
            {
                University university = new University()
                {
                    UniversityCode = i.ToString("00000"),
                    UniversityName = String.Format("UniversityName-{0}", i.ToString("00000")),
                    CurrentTuitionFee = GenerateRandomFee(25000, 100000),
                    CurrentTermId = _termService.GetCurrentTerm().Id,
                    Students = new List<Student>()
                };
                _universityService.Insert(university);
            }
            string message = " Universities Generated: " + limit;
            return message;
        }

        private string GenerateUniversityBankAccounts()
        {
            List<University> universities = _universityService.Get().ToList();
            int count = 0;
            foreach (var university in universities)
            {
                count++;
                BankAccount bankAccount = new()
                {
                    AccountCode = count.ToString("C-000000000"),
                    AccountType = Model.Enums.BankAccountType.Commercial,
                    Balance = 0
                };
                _bankAccountService.Insert(bankAccount);
                university.BankAccountId = bankAccount.Id;
                _universityService.Update(university);
            }
            return " University Bank Accounts Generated: " + count;
        }

        private string GenerateStudents(int limit)
        {
            List<University> universities = _universityService.Get().ToList();
            string yearStr = DateTime.Now.Year.ToString();
            string monthStr = DateTime.Now.Month.ToString("00");
            int count = 0;
            foreach (var university in universities)
            {
                for(int i = 0; i < limit; i++)
                {
                    count++;
                    Student student = new Student()
                    {
                        StudentNo = String.Format("{0}{1}{2}", yearStr.Substring(yearStr.Length - 2), monthStr.ToString(), count.ToString("00000")),
                        StudentName = String.Format("StudentName-{0}", count.ToString("00000")),
                        TCKimlikNo = String.Format("TCKN-{0}", count.ToString("000000")),
                        CurrentUniversityId = university.Id,
                        Status = Model.Enums.StudentStatus.PaymentPending
                    };
                    university.Students.Add(student);
                }
                _universityService.Update(university);
            }
            return String.Format(" Students Generated. {0} per university. ", limit.ToString());
        }

        private string GenerateStudentBankAccounts()
        {
            List<Student> students = _studentService.Get().ToList();
            int count = 0;
            foreach(var student in students)
            {
                count++;
                BankAccount bankAccount = new()
                {
                    AccountCode = count.ToString("P-000000000"),
                    TCKimlikNo = student.TCKimlikNo,
                    AccountType = Model.Enums.BankAccountType.Personal,
                    Balance = 0
                };
                _bankAccountService.Insert(bankAccount);
            }
            return " Student Bank Accounts Generated: " + count;
        }

        private string GenerateTuitions()
        {
            List<University> universities = _universityService.Get().ToList();
            foreach(var university in universities)
            {
                List<Student> students = university.Students.ToList();
                foreach(var student in students)
                {
                    Tuition tuition = new Tuition()
                    {
                        StudentNo = student.StudentNo,
                        TuitionTotal = university.CurrentTuitionFee,
                        LastPaymentDate = DateTime.Now.AddDays(14), // for a test
                        TermId = university.CurrentTermId,
                        Status = Model.Enums.TuitionStatus.Pending
                    };
                    _tuitionService.Insert(tuition);
                }
            }
            return " Tuitions Generated For All Existing Students. ";
        }

        private double GenerateRandomFee(double min, double max)
        {
            Random random = new Random();
            return random.NextDouble() * (max - min) + min;
        }

    }
}
