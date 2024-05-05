using UniversityTuitionPaymentV2.Context;
using UniversityTuitionPaymentV2.Model;
using UniversityTuitionPaymentV2.Source.Db;

namespace UniversityTuitionPaymentV2.Source.Services
{
    public class UniversityService : IUniversityService
    {
        private UniversityTuitionPaymentContext _context;

        public UniversityService(UniversityTuitionPaymentContext context)
        {
            _context = context;
        }

        public int Delete()
        {
            UniversityAccess access = new UniversityAccess(_context);
            return access.deleteUniversities();
        }

        public int Delete(int id)
        {
            UniversityAccess access = new UniversityAccess(_context);
            return access.deleteUniversity(id);
        }

        public ICollection<University> Get()
        {
            UniversityAccess access = new UniversityAccess(_context);
            return access.GetUniversities().ToList();
        }

        public University Get(int id)
        {
            UniversityAccess access = new UniversityAccess(_context);
            return access.GetUniversity(id);
        }

        public int Insert(University item)
        {
            UniversityAccess access = new UniversityAccess(_context);
            return access.insertUniversity(item);
        }

        public int Update(University item)
        {
            UniversityAccess access = new UniversityAccess(_context);
            return access.updateUniversity(item);
        }

        public List<Student> StudentsEnrolled(int id)
        {
            University university = Get(id);
            List<Student> students = [.. university.Students];
            return students;
        }

        public int EnrollStudent(int id, Student student)
        {
            University university = Get(id);
            university.Students.Add(student);
            return Update(university);
        }
    }
}
