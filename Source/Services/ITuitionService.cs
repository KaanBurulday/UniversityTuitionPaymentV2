using UniversityTuitionPaymentV2.Model;

namespace UniversityTuitionPaymentV2.Source.Services
{
    public interface ITuitionService
    {
        ICollection<Tuition> Get();
        public Tuition Get(int id);
        public Tuition Get(string studentNo);
        public Tuition GetTuitionByStudentNoAndTerm(string studentNo, Term term);
        public int Insert(Tuition tuition);
        public int Update(Tuition tuition);
        public int Delete();
        public int Delete(int id);
    }
}
