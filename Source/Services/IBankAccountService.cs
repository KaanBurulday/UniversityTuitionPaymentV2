using UniversityTuitionPaymentV2.Model;
using UniversityTuitionPaymentV2.Model.Enums;

namespace UniversityTuitionPaymentV2.Source.Services
{
    public interface IBankAccountService
    {
        ICollection<BankAccount> Get();
        public BankAccount Get(int id);
        public BankAccount GetByTCKN(string tckn);
        public BankAccount GetBankAccountByAccountCode(string accountCode);
        public int Insert(BankAccount bankAccount);
        public int Update(BankAccount bankAccount);
        public int Delete();
        public int Delete(int id);
        public double AddBalance(BankAccount bankAccount, double amount);
        public double SubtractBalance(BankAccount bankAccount, double amount);
        public double Transfer(BankAccount sender, BankAccount receiver, double amount);
    }
}
