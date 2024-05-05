using Microsoft.AspNetCore.Mvc;
using UniversityTuitionPaymentV2.Model;
using UniversityTuitionPaymentV2.Source.Services;

namespace UniversityTuitionPaymentV2.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "3-Generic Controller")]
    public class StudentController : ControllerBase
    {
        private IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public List<StudentDto> Get()
        {
            List<Student> datas = _studentService.Get().ToList();
            List<StudentDto> ret = new List<StudentDto>();
            datas.ForEach(data => ret.Add(createStudentDto(data)));
            return ret;
        }

        [HttpGet("{id}")]
        public StudentDto GetStudentById(int id)
        {
            Student data = _studentService.Get(id);
            return createStudentDto(data);
        }
        
        [HttpPost("InsertStudent")]
        public StudentResultDto InsertStudent([FromBody] StudentDto student)
        {
            StudentResultDto ret = new StudentResultDto();
            if (!ModelState.IsValid)
            {
                ret.Status = "FAILURE";
                ret.Message = "Invalid Model";
                return ret;
            }
            try
            {
                int studentId = _studentService.Insert(createStudent(student));
                if (studentId != -1)
                {
                    ret.Id = _studentService.Get(studentId).Id;
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
        public int UpdateStudent([FromBody] StudentDto student)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("Delete")]
        public int DeleteAll()
        {
            return _studentService.Delete();
        }

        [HttpDelete("Delete/{id}")]
        public int Delete(int id)
        {
            return _studentService.Delete(id);
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

        private Student createStudent(StudentDto studentDto)
        {
            Student ret = new Student()
            {
                StudentNo = studentDto.StudentNo,
                TCKimlikNo = studentDto.TCKimlikNo,
                StudentName = studentDto.StudentName,
                CurrentUniversityId = studentDto.CurrentUniversityId,
                Status = studentDto.Status
            };
            return ret;
        }
    }
}
