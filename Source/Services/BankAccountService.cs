using Microsoft.EntityFrameworkCore;
using System.Transactions;
using UniversityTuitionPaymentV2.Context;
using UniversityTuitionPaymentV2.Model;
using UniversityTuitionPaymentV2.Model.Enums;
using UniversityTuitionPaymentV2.Source.Db;

namespace UniversityTuitionPaymentV2.Source.Services
{
    public class BankAccountService : IBankAccountService
    {
        private UniversityTuitionPaymentContext _context;

        public BankAccountService(UniversityTuitionPaymentContext context)
        {
            _context = context;
        }

        public double AddBalance(BankAccount bankAccount, double amount)
        {
            bankAccount.Balance += amount;
            Update(bankAccount);
            return bankAccount.Balance;
        }

        public double SubtractBalance(BankAccount bankAccount, double amount)
        {
            if (bankAccount.Balance < amount)
                return -1;
            bankAccount.Balance -= amount;
            Update(bankAccount);
            return bankAccount.Balance;
        }

        public double Transfer(BankAccount sender, BankAccount receiver, double amount)
        {
            double transferAmount = amount;
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if(sender.Balance < amount)
                        transferAmount = sender.Balance;
                    SubtractBalance(sender, transferAmount);
                    AddBalance(receiver, transferAmount);
                    transaction.Commit();
                    return transferAmount;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Transfer failed", ex);
                }
            }
        }

        //public int Transfer(BankAccount sender, BankAccount receiver, double amount)
        //{
        //    BankAccount copySender = sender;
        //    BankAccount copyReceiever = receiver;
        //    try
        //    {
        //        if(sender.Balance < amount)
        //        {
        //            return -2;
        //        }
        //        SubtractBalance(sender, amount);
        //        AddBalance(receiver, amount);
        //        return 1;
        //    } 
        //    catch (Exception)
        //    {
        //        Update(copySender);
        //        Update(copyReceiever);
        //        return -1;
        //    }
        //}

        public int Delete()
        {
            BankAccountAccess access = new BankAccountAccess(_context);
            return access.deleteBankAccounts();
        }

        public int Delete(int id)
        {
            BankAccountAccess access = new BankAccountAccess(_context);
            return access.deleteBankAccount(id);
        }

        public ICollection<BankAccount> Get()
        {
            BankAccountAccess access = new BankAccountAccess(_context);
            return access.GetBankAccounts().ToList();
        }

        public BankAccount Get(int id)
        {
            BankAccountAccess access = new BankAccountAccess(_context);
            return access.GetBankAccount(id);
        }

        public BankAccount GetBankAccountByAccountCode(string accountCode)
        {
            BankAccountAccess access = new BankAccountAccess(_context);
            return access.GetBankAccountByAccountCode(accountCode);
        }

        public BankAccount GetByTCKN(string tckn)
        {
            BankAccountAccess access = new BankAccountAccess(_context);
            return access.GetBankAccountByTCKN(tckn);
        }

        public int Insert(BankAccount item)
        {
            BankAccountAccess access = new BankAccountAccess(_context);
            return access.insertBankAccount(item);
        }

        public int Update(BankAccount item)
        {
            BankAccountAccess access = new BankAccountAccess(_context);
            return access.updateBankAccount(item);
        }
    }
}
