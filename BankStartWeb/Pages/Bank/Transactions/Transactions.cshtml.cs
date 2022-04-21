using BankStartWeb.Data;
using BankStartWeb.Infrastructure.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace BankStartWeb.Pages.Bank.Transactions
{
    [Authorize]
    public class TransactionsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public TransactionsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<TransactionsViewModel> Transactions { get; set; }
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
        public void OnGet(int accountId, int customerId)
        {
            var transaction = _context.Accounts
                .Include(n => n.Transactions)
                .First(n => n.Id == accountId);

            var customerid = _context.Customers.FirstOrDefault(e => e.Id == CustomerId);

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

        //public IActionResult OnGetFetchMore(int transactionId, int pageNo)
        //{
        //    var query = _context.Accounts.Where(e => e.Id == transactionId)
        //        .SelectMany(e => e.Transactions)
        //        .OrderBy(e => e.Amount)
        //        ;
        //    var r = query.GetPaged(pageNo, 5);

        //    var list = r.Results.Select(e => new TransactionsViewModel
        //    {
        //        Id = e.Id,
        //        Amount = e.Amount,
        //        Type = e.Type,
        //        Operation = e.Operation,
        //        Date = e.Date,
        //        NewBalance = e.NewBalance
        //    }).ToList();

        //    bool lastPage = pageNo == r.PageCount;

        //    return new JsonResult(new { items = list, lastPage = lastPage });
        //}
    }
}
