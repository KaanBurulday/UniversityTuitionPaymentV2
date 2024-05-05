using UniversityTuitionPaymentV2.Model;

namespace UniversityTuitionPaymentV2.Source.Services
{
    public interface IStudentService
    {
        ICollection<Student> Get();
        public Student Get(int id);
        public Student Get(string studentNo);
        public int Insert(Student student);
        public int Update(Student student);
        public int Delete();
        public int Delete(int id);
    }
}
