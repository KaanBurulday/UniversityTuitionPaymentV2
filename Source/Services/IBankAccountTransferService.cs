using UniversityTuitionPaymentV2.Model;

namespace UniversityTuitionPaymentV2.Source.Services
{
    public interface IBankAccountTransferService
    {
        ICollection<BankAccountTransfer> Get();
        public BankAccountTransfer Get(int id);
        public int Insert(BankAccountTransfer bankAccountTransfer); 
        public int Update(BankAccountTransfer bankAccountTransfer);
        public int Delete();
        public int Delete(int id);
        public BankAccountTransfer MakeTransfer(BankAccountTransfer bankAccountTransfer);
        public BankAccountTransfer CreateBankAccountTransfer(string senderCode, string receiverCode, double amount);
    }
}
