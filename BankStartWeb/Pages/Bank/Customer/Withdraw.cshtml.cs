using System.ComponentModel.DataAnnotations;
using BankStartWeb.Data;
using BankStartWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Frameworks;

namespace BankStartWeb.Pages.Bank.Customer
{
    [Authorize(Roles = "Cashier, Admin")]
    public class WithdrawModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountService _accountService;

        public WithdrawModel(ApplicationDbContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }
        [BindProperty]
        public int AccountId { get; set; }
        [BindProperty]
        [Range(1,8000)]
        public decimal Amount { get; set; }

        public void OnGet(int accountId)
        {
            var account = _context.Accounts
                .FirstOrDefault(e => e.Id == accountId);

            var id = account.Id;
        }

        public IActionResult OnPost(int accountId)
        {
            if (ModelState.IsValid)
            {
                var status = _accountService.WithDraw(accountId, Amount);

                if (status == IAccountService.ErrorCode.ok)
                {
                    TempData["success"] = "Transaction went successful!";
                    return RedirectToPage("/Bank/Transactions/Transactions", new {accountId = AccountId});
                }
            }

            return Page();
        }
    }
}
