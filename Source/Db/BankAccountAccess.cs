using UniversityTuitionPaymentV2.Context;
using UniversityTuitionPaymentV2.Model;

namespace UniversityTuitionPaymentV2.Source.Db
{
    public class BankAccountAccess
    {
        private UniversityTuitionPaymentContext _context;

        public BankAccountAccess(UniversityTuitionPaymentContext context)
        {
            _context = context;
        }

        public IEnumerable<BankAccount> GetBankAccounts()
        {
            return _context.BankAccounts;
        }

        public BankAccount GetBankAccount(int id)
        {
            return _context.BankAccounts.FirstOrDefault(u => u.Id == id);
        }

        public BankAccount GetBankAccountByTCKN(string tckn)
        {
            return _context.BankAccounts.FirstOrDefault(u => u.TCKimlikNo == tckn);
        }

        public BankAccount GetBankAccountByAccountCode(string accountCode)
        {
            return _context.BankAccounts.FirstOrDefault(u => u.AccountCode == accountCode);
        }

        public int insertBankAccount(BankAccount bankAccount)
        {
            validateBankAccount(bankAccount);
            _context.BankAccounts.Add(bankAccount);
            _context.SaveChanges();
            return bankAccount.Id;
        }

        public int updateBankAccount(BankAccount bankAccount)
        {
            validateBankAccount(bankAccount);
            BankAccount bankAccountOld = GetBankAccount(bankAccount.Id);
            if (bankAccountOld != null)
            {
                bankAccountOld.AccountCode = bankAccount.AccountCode;
                bankAccountOld.Balance = bankAccount.Balance;
                bankAccountOld.TCKimlikNo = bankAccount.TCKimlikNo;
                _context.SaveChanges();
                return bankAccount.Id;
            }
            return -1;
        }

        public int deleteBankAccount(int id)
        {
            BankAccount data = GetBankAccount(id);
            if (data != null)
            {
                _context.BankAccounts.Remove(data);
                return _context.SaveChanges();
            }
            return 0;
        }

        public int deleteBankAccounts()
        {
            foreach (var entity in _context.BankAccounts)
            {
                _context.BankAccounts.Remove(entity);
            }
            return _context.SaveChanges();
        }

        private void validateBankAccount(BankAccount bankAccount)
        {

        }
    }
}
