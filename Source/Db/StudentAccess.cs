using UniversityTuitionPaymentV2.Context;
using UniversityTuitionPaymentV2.Model;

namespace UniversityTuitionPaymentV2.Source.Db
{
    public class StudentAccess
    {
        private UniversityTuitionPaymentContext _context;

        public StudentAccess(UniversityTuitionPaymentContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _context.Students;
        }

        public Student GetStudent(int id)
        {
            return _context.Students.FirstOrDefault(u => u.Id == id);
        }

        public Student GetStudentByNo(string studentNo)
        {
            return _context.Students.FirstOrDefault(u => u.StudentNo == studentNo);
        }

        public int insertStudent(Student student)
        {
            validateStudent(student);
            _context.Students.Add(student);
            _context.SaveChanges();
            return student.Id;
        }

        public int updateStudent(Student student)
        {
            validateStudent(student);
            Student studentOld = GetStudent(student.Id);
            if (studentOld != null)
            {
                studentOld.StudentNo = student.StudentNo;
                studentOld.TCKimlikNo = student.TCKimlikNo;
                studentOld.StudentName = student.StudentName;
                studentOld.CurrentUniversityId = student.CurrentUniversityId;
                studentOld.Status = student.Status;
                _context.SaveChanges();
                return student.Id;
            }
            return -1;
        }

        public int deleteStudent(int id)
        {
            Student data = GetStudent(id);
            if (data != null)
            {
                _context.Students.Remove(data);
                return _context.SaveChanges();
            }
            return 0;
        }

        public int deleteStudents()
        {
            foreach (var entity in _context.Students)
            {
                _context.Students.Remove(entity);
            }
            return _context.SaveChanges();
        }

        private void validateStudent(Student student)
        {
            // throw new NotImplementedException();
        }
    }
}
