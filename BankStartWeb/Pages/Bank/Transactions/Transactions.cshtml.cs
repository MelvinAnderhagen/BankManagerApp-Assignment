using System.ComponentModel.DataAnnotations;
using BankStartWeb.Data;
using BankStartWeb.Infrastructure.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace BankStartWeb.Pages.Bank.Transactions
{
    [Authorize(Roles = "Admin, Cashier")]
    public class TransactionsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public TransactionsModel(ApplicationDbContext context)
        {
            _context = context;
        }

            public List<TransactionViewModel> Transactions { get; set; }

            public int Accountid { get; set; }

            public class TransactionViewModel
            {
                public int Id { get; set; }

                [MaxLength(10)]
                public string Type { get; set; }
                [MaxLength(50)]
                public string Operation { get; set; }
                public DateTime Date { get; set; }
                public decimal Amount { get; set; }
                public decimal NewBalance { get; set; }
            }
            
            public void OnGet(int accountId)
            {
                var transaction = _context.Customers.Include(e => e.Accounts)
                    .ThenInclude(e => e.Transactions.OrderByDescending(e => e.Id))
                    .FirstOrDefault(e => e.Accounts.Any(e => e.Id == accountId));


                var account = _context.Accounts.FirstOrDefault(e => e.Id == accountId);

                Accountid = accountId;

                Transactions = account.Transactions.Select(e => new TransactionViewModel
                {
                    Id = e.Id,
                    Type = e.Type,
                    Operation = e.Operation,
                    Date = e.Date,
                    Amount = e.Amount,
                    NewBalance = e.NewBalance
                }).OrderBy(e => e.Id).ToList();
            }

            public IActionResult OnGetFetchMore(int accountId, int pageNo)
            {
                var query = _context.Accounts.Where(e => e.Id == accountId)
                    .SelectMany(e => e.Transactions)
                    .OrderBy(e => e.Amount);

                var r = query.GetPaged(pageNo, 5);

                var list = r.Results.Select(e => new TransactionViewModel
                {
                    Id = e.Id,
                    Amount = e.Amount,
                    Type = e.Type,
                    Operation = e.Operation,
                    Date = e.Date,
                    NewBalance = e.NewBalance
                }).ToList().OrderByDescending(e => e.Date);

                bool lastPage = pageNo == r.PageCount;

                return new JsonResult(new { items = list, lastPage = lastPage });
            }
    }
}
