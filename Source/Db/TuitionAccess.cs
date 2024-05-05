using UniversityTuitionPaymentV2.Context;
using UniversityTuitionPaymentV2.Model;

namespace UniversityTuitionPaymentV2.Source.Db
{
    public class TuitionAccess
    {
        private UniversityTuitionPaymentContext _context;

        public TuitionAccess(UniversityTuitionPaymentContext context)
        {
            _context = context;
        }

        public IEnumerable<Tuition> GetTuitions()
        {
            return _context.Tuitions;
        }

        public Tuition GetTuition(int id)
        {
            return _context.Tuitions.FirstOrDefault(u => u.Id == id);
        }

        public Tuition GetTuitionByStudentNo(string studentNo)
        {
            return _context.Tuitions.OrderByDescending(u => u.Id).FirstOrDefault(u => (u.StudentNo == studentNo) && (u.Status != Model.Enums.TuitionStatus.Successful));
        }

        public Tuition GetTuitionByStudentNoAndTerm(string studentNo, Term term)
        {
            return _context.Tuitions.OrderByDescending(u => u.Id).FirstOrDefault(u => (u.StudentNo == studentNo) && (u.Term == term) && (u.Status != Model.Enums.TuitionStatus.Successful));
        }

        public int insertTuitions(Tuition tuition)
        {
            validateTuition(tuition);
            _context.Tuitions.Add(tuition);
            _context.SaveChanges();
            return tuition.Id;
        }

        public int updateTuition(Tuition tuition)
        {
            validateTuition(tuition);
            Tuition tuitionOld = GetTuition(tuition.Id);
            if (tuitionOld != null)
            {
                tuitionOld.TuitionTotal = tuition.TuitionTotal;
                tuitionOld.StudentNo = tuition.StudentNo;
                tuitionOld.LastPaymentDate = tuition.LastPaymentDate;
                tuitionOld.Status = tuition.Status;
                tuitionOld.TermId = tuition.TermId;
                _context.SaveChanges();
                return tuition.Id;
            }
            return -1;
        }

        public int deleteTuition(int id)
        {
            Tuition data = GetTuition(id);
            if (data != null)
            {
                _context.Tuitions.Remove(data);
                return _context.SaveChanges();
            }
            return 0;
        }

        public int deleteTuitions()
        {
            foreach (var entity in _context.Tuitions)
            {
                _context.Tuitions.Remove(entity);
            }
            return _context.SaveChanges();
        }

        private void validateTuition(Tuition tuition)
        {
            // throw new NotImplementedException();
        }
    }
}
