using UniversityTuitionPaymentV2.Context;
using UniversityTuitionPaymentV2.Model;

namespace UniversityTuitionPaymentV2.Source.Db
{
    public class TermAccess
    {
        private UniversityTuitionPaymentContext _context;

        public TermAccess(UniversityTuitionPaymentContext context)
        {
            _context = context;
        }

        public IEnumerable<Term> GetTerms()
        {
            return _context.Terms;
        }

        public Term GetTerm(int id)
        {
            return _context.Terms.FirstOrDefault(u => u.Id == id);
        }

        public Term GetByYears(int start, int end)
        {
            return _context.Terms.FirstOrDefault(u => (u.StartYear == start) && (u.EndYear == end));
        }

        public Term GetCurrentTerm()
        {
            return _context.Terms.OrderByDescending(u => u.Id).FirstOrDefault();
        }

        public int insertTerm(Term term)
        {
            validateTerm(term);
            _context.Terms.Add(term);
            _context.SaveChanges();
            return term.Id;
        }

        public int updateTerm(Term term)
        {
            validateTerm(term);
            Term termOld = GetTerm(term.Id);
            if (termOld != null)
            {
                termOld.StartYear = term.StartYear;
                termOld.EndYear = term.EndYear;
                _context.SaveChanges();
                return term.Id;
            }
            return -1;
        }

        public int deleteTerm(int id)
        {
            Term data = GetTerm(id);
            if (data != null)
            {
                _context.Terms.Remove(data);
                return _context.SaveChanges();
            }
            return 0;
        }

        public int deleteTerms()
        {
            foreach (var entity in _context.Terms)
            {
                _context.Terms.Remove(entity);
            }
            return _context.SaveChanges();
        }

        private void validateTerm(Term term)
        {
            // throw new NotImplementedException();
        }
    }
}
