using System.ComponentModel.DataAnnotations;
using BankStartWeb.Data;
using BankStartWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Bank.Customer
{
    public class DepositModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountService _accountService;

        public DepositModel(ApplicationDbContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }
        [Range(1, 10000)]
        public decimal Amount { get; set; }
        public string Type { get; set; }

        public void OnGet(int accountId)
        {
            var account = _context.Accounts.FirstOrDefault(e => e.Id == accountId);

            var id = account.Id;
        }

        public IActionResult OnPost(int accountId, decimal amount)
        {
            if (ModelState.IsValid)
            {
                var status = _accountService.Deposit(accountId, amount);

                if (status == IAccountService.ErrorCode.ok)
                {
                    return RedirectToPage("/Bank/Transactions/Transactions", new {id = accountId});
                }
                
                ModelState.AddModelError("amount", "invalid amount");

               
            }

            return Page();
        }
    }
}
