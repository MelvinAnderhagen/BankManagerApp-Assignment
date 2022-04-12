using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BankStartWeb.Pages.Bank
{
    public class TransactionsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public TransactionsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<TransactionsViewModel> Transactions { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public class TransactionsViewModel
        {
            public int Id { get; set; }
            public string Type { get; set; }
            public string Operation { get; set; }
            public DateTime Date { get; set; }
            public decimal Amount { get; set; }
            public decimal NewBalance { get; set; }
        }
        public void OnGet(int accountId)
        {
            var transaction = _context.Accounts
                .Include(n => n.Transactions)
                .First(n => n.Id == accountId);

            

            //Givenname = transaction.Givenname;
            //Surname = transaction.Surname;
            //CustomerId = transaction.Id;

            Transactions = transaction.Transactions.Select(n => new TransactionsViewModel
            {
                Amount = n.Amount,
                NewBalance = n.NewBalance,
                Type = n.Type,
                Operation = n.Operation,
                Date = n.Date,
                Id = n.Id

            }).ToList();
        }

    }
}
