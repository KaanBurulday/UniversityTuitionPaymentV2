using UniversityTuitionPaymentV2.Context;
using UniversityTuitionPaymentV2.Model;
using UniversityTuitionPaymentV2.Source.Db;

namespace UniversityTuitionPaymentV2.Source.Services
{
    public class BankAccountTransferService : IBankAccountTransferService
    {
        private UniversityTuitionPaymentContext _context;
        private IBankAccountService _bankAccountService;

        public BankAccountTransferService(UniversityTuitionPaymentContext context, IBankAccountService bankAccountService)
        {
            _context = context;
            _bankAccountService = bankAccountService;
        }

        public BankAccountTransfer MakeTransfer(BankAccountTransfer bankAccountTransfer)
        {
            try
            {
                bankAccountTransfer.Status = Model.Enums.TransferStatus.Processing;
                Update(bankAccountTransfer);

                BankAccount sender = _bankAccountService.GetBankAccountByAccountCode(bankAccountTransfer.SenderCode);
                BankAccount receiver = _bankAccountService.GetBankAccountByAccountCode(bankAccountTransfer.ReceiverCode);
                double amount = bankAccountTransfer.TransferAmount;

                double transferedAmount = _bankAccountService.Transfer(sender: sender, receiver: receiver, amount: amount);

                if (transferedAmount != amount)
                {
                    bankAccountTransfer.TransferAmount = transferedAmount;
                    bankAccountTransfer.Status = Model.Enums.TransferStatus.Pending;
                    bankAccountTransfer.StatusMessage = String.Format("The transfer is not successful due to insufficient funds. Amount left: {0}",
                                                                        amount - transferedAmount);
                } 
                else
                {
                    bankAccountTransfer.Status = Model.Enums.TransferStatus.Successful;
                    bankAccountTransfer.StatusMessage = "Transfer Successful";
                }                
                Update(bankAccountTransfer);
                return bankAccountTransfer;
            } 
            catch (Exception e)
            {
                bankAccountTransfer.StatusMessage = String.Format("Error occured while transfering. Date: {0}. Message: {1}"
                                                                        , DateTime.Now.ToString()
                                                                        , e.Message);
                bankAccountTransfer.Status = Model.Enums.TransferStatus.Error;
                Update(bankAccountTransfer);
                return bankAccountTransfer;
            }
        }

        public BankAccountTransfer CreateBankAccountTransfer(string senderCode, string receiverCode, double amount)
        {
            BankAccountTransfer bankAccountTransfer = new BankAccountTransfer()
            {
                SenderCode = senderCode,
                ReceiverCode = receiverCode,
                TransferAmount = amount,
                Status = Model.Enums.TransferStatus.Pending
            };
            return bankAccountTransfer;
        }

        public int Delete()
        {
            BankAccountTransferAccess access = new BankAccountTransferAccess(_context);
            return access.deleteBankAccountTransfers();
        }

        public int Delete(int id)
        {
            BankAccountTransferAccess access = new BankAccountTransferAccess(_context);
            return access.deleteBankAccountTransfer(id);
        }

        public ICollection<BankAccountTransfer> Get()
        {
            BankAccountTransferAccess access = new BankAccountTransferAccess(_context);
            return access.GetBankAccountTransfers().ToList();
        }

        public BankAccountTransfer Get(int id)
        {
            BankAccountTransferAccess access = new BankAccountTransferAccess(_context);
            return access.GetBankAccountTransfer(id);
        }

        public int Insert(BankAccountTransfer item)
        {
            BankAccountTransferAccess access = new BankAccountTransferAccess(_context);
            return access.insertBankAccountTransfer(item);
        }

        public int Update(BankAccountTransfer bankAccountTransfer)
        {
            BankAccountTransferAccess access = new BankAccountTransferAccess(_context);
            return access.updateBankAccountTransfer(bankAccountTransfer);
        }
    }
}
