using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BankStartWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public int CountCustomers { get; set; }
        public int CountAccounts { get; set; }
        public int CountTransactions { get; set; }
        public List<AccountViewModel> Accounts { get; set; }
        public decimal TotalBalance { get; set; } = 0;
        public string Prefix { get; set; }
        public class AccountViewModel
        {
            public decimal Balance { get; set; }
        }

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            var customers = _context.Customers.Count();

            var accounts = _context.Accounts.Count();

            var transactions = _context.Transactions.Count();

            Accounts = _context.Accounts.Select(x => new AccountViewModel
            {
                Balance = x.Balance
            }).ToList();

            foreach (var item in Accounts)
            {
                TotalBalance += item.Balance;
            }

            if (TotalBalance >= 1000000)
            {
                
            }

            CountTransactions = transactions;
            CountAccounts = accounts;
            CountCustomers = customers;
            


        }
    }
}