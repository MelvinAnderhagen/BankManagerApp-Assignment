using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IAccountService.ErrorCode Deposit(int accountId, decimal amount)
        {
            if (amount < 0)
            {
                return IAccountService.ErrorCode.AmountIsNegative;
            }

            var account = _context.Accounts
                .Include(e => e.Transactions)
                .FirstOrDefault(e => e.Id == accountId);

            account.Balance += amount;

            var transaction = new Transaction();
            transaction.Type = "Debit";
            transaction.Amount = amount;
            transaction.Operation = "Deposit";
            transaction.Date = DateTime.Now;
            transaction.NewBalance = account.Balance;
            
            account.Transactions.Add(transaction);
            _context.SaveChanges();

            return IAccountService.ErrorCode.ok;
        }

        public IAccountService.ErrorCode WithDraw(int accountId, decimal amount, string type)
        {
            var account = _context.Accounts.FirstOrDefault(e => e.Id == accountId);

            if (account.Balance < amount)
            {
                return IAccountService.ErrorCode.BalanceIsToLow;
            }
            else if (amount < 0)
            {
                return IAccountService.ErrorCode.AmountIsNegative;
            }

            account.Balance -= amount;

            var transaction = new Transaction();
            transaction.Type = type;
            transaction.Amount = amount;
            transaction.Operation = "Withdraw";
            transaction.Date = DateTime.Now;
            transaction.NewBalance = account.Balance;
            account.Transactions.Add(transaction);
            _context.SaveChanges();

            return IAccountService.ErrorCode.ok;
        }
    }
}
