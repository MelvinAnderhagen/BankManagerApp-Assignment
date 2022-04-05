using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Bank
{
    public class AccountModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AccountModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public string Givenname { get; set; }
        public string Surname { get; set; }
        public int CustomerId { get; set; }
        public List<AccountViewModel> ListOfAccounts { get; set; }
        public class AccountViewModel
        {
            public int AccountId { get; set; }
            public string AccountType { get; set; }
            public DateTime Created { get; set; }
            public decimal Balance { get; set; }
        }
        public void OnGet(int customerId)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Id == customerId);

            var account = _context.Customers.Include(x => x.Accounts).First(x => x.Id == customerId);

            ListOfAccounts = account.Accounts.Select(x => new AccountViewModel
            {
                AccountId = x.Id,
                AccountType = x.AccountType,
                Balance = x.Balance,
                Created = x.Created
            }).ToList();

            CustomerId = customer.Id;
            Surname = customer.Surname;
            Givenname = customer.Givenname;


        }
    }
}
