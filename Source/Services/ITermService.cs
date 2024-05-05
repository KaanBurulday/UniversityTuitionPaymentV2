using UniversityTuitionPaymentV2.Model;

namespace UniversityTuitionPaymentV2.Source.Services
{
    public interface ITermService
    {
        public Term GetCurrentTerm();
        ICollection<Term> Get();
        public Term Get(int id);
        public Term GetByYears(int start, int end);
        public int Insert(Term term);
        public int Update(Term term);
        public int Delete();
        public int Delete(int id);
    }
}
