using UniversityTuitionPaymentV2.Context;
using UniversityTuitionPaymentV2.Model;

namespace UniversityTuitionPaymentV2.Source.Db
{
    public class BankAccountTransferAccess
    {
        private UniversityTuitionPaymentContext _context;

        public BankAccountTransferAccess(UniversityTuitionPaymentContext context)
        {
            _context = context;
        }

        public IEnumerable<BankAccountTransfer> GetBankAccountTransfers()
        {
            return _context.BankAccountTransfers;
        }

        public BankAccountTransfer GetBankAccountTransfer(int id)
        {
            return _context.BankAccountTransfers.FirstOrDefault(u => u.Id == id);
        }

        public int insertBankAccountTransfer(BankAccountTransfer bankAccountTransfer)
        {
            validateBankAccountTransfer(bankAccountTransfer);
            _context.BankAccountTransfers.Add(bankAccountTransfer);
            _context.SaveChanges();
            return bankAccountTransfer.Id;
        }

        public int updateBankAccountTransfer(BankAccountTransfer bankAccountTransfer)
        {
            validateBankAccountTransfer(bankAccountTransfer);
            BankAccountTransfer bankAccountTransferOld = GetBankAccountTransfer(bankAccountTransfer.Id);
            if (bankAccountTransferOld != null)
            {
                bankAccountTransferOld.SenderCode = bankAccountTransfer.SenderCode;
                bankAccountTransferOld.ReceiverCode = bankAccountTransfer.ReceiverCode;
                bankAccountTransferOld.TransferAmount = bankAccountTransfer.TransferAmount;
                bankAccountTransferOld.Status = bankAccountTransfer.Status;
                bankAccountTransferOld.StatusMessage = bankAccountTransfer.StatusMessage;
                _context.SaveChanges();
                return bankAccountTransfer.Id;
            }
            return -1;
        }

        public int deleteBankAccountTransfer(int id)
        {
            BankAccountTransfer data = GetBankAccountTransfer(id);
            if (data != null)
            {
                _context.BankAccountTransfers.Remove(data);
                return _context.SaveChanges();
            }
            return 0;
        }

        public int deleteBankAccountTransfers()
        {
            foreach (var entity in _context.BankAccountTransfers)
            {
                _context.BankAccountTransfers.Remove(entity);
            }
            return _context.SaveChanges();
        }

        private void validateBankAccountTransfer(BankAccountTransfer bankAccountTransfer)
        {
            // throw new NotImplementedException();
        }
    }
}
