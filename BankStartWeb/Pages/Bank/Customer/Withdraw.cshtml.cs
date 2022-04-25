using BankStartWeb.Data;
using BankStartWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Frameworks;

namespace BankStartWeb.Pages.Bank.Customer
{
    public class WithdrawModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountService _accountService;

        public WithdrawModel(ApplicationDbContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }

        public void OnGet(int accountId)
        {
            var account = _context.Accounts.FirstOrDefault(e => e.Id == accountId);

            var id = account.Id;
        }

        public IActionResult OnPost(int accountId, decimal amount, string type)
        {
            if (ModelState.IsValid)
            {
                var status = _accountService.WithDraw(accountId, amount, type);

                if (status == IAccountService.ErrorCode.ok)
                {
                    return RedirectToPage("/Bank/Transactions/Transactions", new {accountId = AccountId});
                }
            }

            return Page();
        }
    }
}
