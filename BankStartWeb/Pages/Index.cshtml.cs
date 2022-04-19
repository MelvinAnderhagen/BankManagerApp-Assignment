using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public int CountCustomers { get; set; }
        public int CountAccounts { get; set; }
        public int CountTransactions { get; set; }

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



            CountTransactions = transactions;
            CountAccounts = accounts;
            CountCustomers = customers;

        }
    }
}