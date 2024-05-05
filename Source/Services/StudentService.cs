using UniversityTuitionPaymentV2.Context;
using UniversityTuitionPaymentV2.Model;
using UniversityTuitionPaymentV2.Source.Db;

namespace UniversityTuitionPaymentV2.Source.Services
{
    public class StudentService : IStudentService
    {
        private UniversityTuitionPaymentContext _context;

        public StudentService(UniversityTuitionPaymentContext context)
        {
            _context = context;
        }

        public int Delete()
        {
            StudentAccess access = new StudentAccess(_context);
            return access.deleteStudents();
        }

        public int Delete(int id)
        {
            StudentAccess access = new StudentAccess(_context);
            return access.deleteStudent(id);
        }

        public ICollection<Student> Get()
        {
            StudentAccess access = new StudentAccess(_context);
            return access.GetStudents().ToList();
        }

        public Student Get(int id)
        {
            StudentAccess access = new StudentAccess(_context);
            return access.GetStudent(id);
        }

        public Student Get(string studentNo)
        {
            StudentAccess access = new StudentAccess(_context);
            return access.GetStudentByNo(studentNo);
        }

        public int Insert(Student item) 
        {
            StudentAccess access = new StudentAccess(_context);
            return access.insertStudent(item);
        }

        public int Update(Student item)
        {
            StudentAccess access = new StudentAccess(_context);
            return access.updateStudent(item);
        }
    }
}