using UniversityTuitionPaymentV2.Model;

namespace UniversityTuitionPaymentV2.Source.Services
{
    public interface IUniversityService
    {
        ICollection<University> Get();
        public University Get(int id);
        public int Insert(University university);
        public int Update(University university);
        public int Delete();
        public int Delete(int id);
        public List<Student> StudentsEnrolled(int id);
        public int EnrollStudent(int id, Student student);
    }
}
