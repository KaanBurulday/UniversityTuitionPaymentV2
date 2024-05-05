using UniversityTuitionPaymentV2.Context;
using UniversityTuitionPaymentV2.Model;
using UniversityTuitionPaymentV2.Source.Db;

namespace UniversityTuitionPaymentV2.Source.Services
{
    public class TuitionService : ITuitionService
    {
        private UniversityTuitionPaymentContext _context;

        public TuitionService(UniversityTuitionPaymentContext context)
        {
            _context = context;
        }

        public int Delete()
        {
            TuitionAccess access = new TuitionAccess(_context);
            return access.deleteTuitions();
        }

        public int Delete(int id)
        {
            TuitionAccess access = new TuitionAccess(_context);
            return access.deleteTuition(id);
        }

        public ICollection<Tuition> Get()
        {
            TuitionAccess access = new TuitionAccess(_context);
            return access.GetTuitions().ToList();
        }

        public Tuition Get(int id)
        {
            TuitionAccess access = new TuitionAccess(_context);
            return access.GetTuition(id);
        }

        public Tuition Get(string studentNo)
        {
            TuitionAccess access = new TuitionAccess(_context);
            return access.GetTuitionByStudentNo(studentNo);
        }

        public Tuition GetTuitionByStudentNoAndTerm(string studentNo, Term term)
        {
            TuitionAccess access = new TuitionAccess(_context);
            return access.GetTuitionByStudentNoAndTerm(studentNo, term);
        }

        public int Insert(Tuition item)
        {
            TuitionAccess access = new TuitionAccess(_context);
            return access.insertTuitions(item);
        }

        public int Update(Tuition item)
        {
            TuitionAccess access = new TuitionAccess(_context);
            return access.updateTuition(item);
        }
    }
}
