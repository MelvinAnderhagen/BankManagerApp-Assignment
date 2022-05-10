using BankStartWeb.Data;
using BankStartWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        [BindProperty]
        public int ReceiverId { get; set; }
        [BindProperty]
        public decimal Amount { get; set; }
        [BindProperty]
        public int AccountId { get; set; }
        public List<Account> Accounts { get; set; }
        public void OnGet(int accountId)
        {
            Accounts = _context.Accounts.Select(a => new Account
            {
                Id = a.Id
            }).ToList();

            AccountId = accountId;
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
