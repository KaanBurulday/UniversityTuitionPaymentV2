using UniversityTuitionPaymentV2.Context;
using UniversityTuitionPaymentV2.Model;
using UniversityTuitionPaymentV2.Source.Db;

namespace UniversityTuitionPaymentV2.Source.Services
{
    public class TermService : ITermService
    {
        private UniversityTuitionPaymentContext _context;

        public TermService(UniversityTuitionPaymentContext context)
        {
            _context = context;
        }

        public Term GetCurrentTerm()
        {
            TermAccess access = new TermAccess(_context);
            return access.GetCurrentTerm();
        }

        public int Delete()
        {
            TermAccess access = new TermAccess(_context);
            return access.deleteTerms();
        }

        public int Delete(int id)
        {
            TermAccess access = new TermAccess(_context);
            return access.deleteTerm(id);
        }

        public ICollection<Term> Get()
        {
            TermAccess access = new TermAccess(_context);
            return access.GetTerms().ToList();
        }

        public Term Get(int id)
        {
            TermAccess access = new TermAccess(_context);
            return access.GetTerm(id);
        }

        public int Insert(Term item)
        {
            TermAccess access = new TermAccess(_context);
            return access.insertTerm(item);
        }

        public int Update(Term item)
        {
            TermAccess access = new TermAccess(_context);
            return access.updateTerm(item);
        }

        public Term GetByYears(int start, int end)
        {
            TermAccess access = new TermAccess(_context);
            return access.GetByYears(start, end);
        }
    }
}
