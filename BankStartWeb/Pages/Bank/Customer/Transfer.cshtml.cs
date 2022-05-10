using BankStartWeb.Data;
using BankStartWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Bank.Customer
{
    [Authorize(Roles = "Cashier, Admin")]
    public class TransferModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountService _accountService;

        public TransferModel(ApplicationDbContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public List<SelectListItem> ListOfAccounts { get; set; } = new List<SelectListItem>();

        [BindProperty]
        public int CustomerID { get; set; }

        [BindProperty]
        public int ReceiverId { get; set; }
        [BindProperty]
        public decimal Amount { get; set; }
        [BindProperty]
        public int AccountId { get; set; }
        public List<Account> Accounts { get; set; }

        

        public void OnGet(int accountId, int customerId)
        {
            var customer = _context.Customers
                .Include(e => e.Accounts)
                .FirstOrDefault(e => e.Id == customerId);

            Accounts = customer.Accounts.Select(a => new Account
            {
                Id = a.Id
            }).ToList();

            CustomerID = customerId;
            
            AccountId = accountId;
            PopulateAccounts();
        }
        public void PopulateAccounts()
        {
            var customer = _context.Customers
                .Include(e => e.Accounts)
                .FirstOrDefault(e => e.Id == CustomerID);

            ListOfAccounts = customer.Accounts.Select(e => new SelectListItem
            {
                Text = e.AccountType + " " + e.Balance + " $",
                Value = e.Id.ToString()
            }).ToList();
        }

        public IActionResult OnPost(int accountId)
        {
            if (ModelState.IsValid)
            {
                var status = _accountService.Transfer(accountId, Amount, ReceiverId);

                if (status == IAccountService.ErrorCode.ok)
                {
                    TempData["success"] = "Transaction went successful!";
                    return RedirectToPage("/Bank/Transactions/Transactions", new { accountId = AccountId });
                }
            }

            return Page();
        }

        
    }

}
