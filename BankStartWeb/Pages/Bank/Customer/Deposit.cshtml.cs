using System.ComponentModel.DataAnnotations;
using BankStartWeb.Data;
using BankStartWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Bank.Customer
{
    [Authorize(Roles = "Cashier, Admin")]
    public class DepositModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountService _accountService;

        public DepositModel(ApplicationDbContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public int AccountId { get; set; }
        [Range(1, 10000)]
        public decimal Amount { get; set; }
       

        public void OnGet(int accountId)
        {
            var account = _context.Accounts.FirstOrDefault(e => e.Id == accountId);

            var id = account.Id;
        }

        public IActionResult OnPost(int accountId, decimal amount)
        {
            AccountId = accountId;

            if (ModelState.IsValid)
            {
                var status = _accountService.Deposit(accountId, amount);

                if (status == IAccountService.ErrorCode.ok)
                {
                    TempData["success"] = "Transaction went successful!";
                    return RedirectToPage("/Bank/Transactions/Transactions", new {accountId = AccountId});
                }
                
                ModelState.AddModelError("amount", "invalid amount");

               
            }

            return Page();
        }
    }
}
