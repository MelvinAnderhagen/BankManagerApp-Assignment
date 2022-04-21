using System.ComponentModel.DataAnnotations;
using BankStartWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Bank.Customer
{
    [Authorize(Roles="Admin")]
    [BindProperties]
    public class EditAccountModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditAccountModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int CustomerId { get; set; }
        public string AccountType { get; set; }

        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        public decimal Balance { get; set; }

        public List<SelectListItem> AllAccountTypes { get; set; } = new List<SelectListItem>();

        public void OnGet(int accountId, int customerId)
        {
            var customer = _context.Customers.FirstOrDefault(e => e.Id == customerId);

            CustomerId = customerId;

            var account = _context.Accounts.FirstOrDefault(e => e.Id == accountId);

            AccountType = account.AccountType;
            Created = account.Created;

            AllAccountTypes.Add(new SelectListItem("Savings", "Savings"));
            AllAccountTypes.Add(new SelectListItem("Checking", "Checking"));
            AllAccountTypes.Add(new SelectListItem("Personal", "Personal"));
        }

        public IActionResult OnPost(int accountId, int customerId)
        {
            if (ModelState.IsValid)
            {
                var account = _context.Accounts
                    .FirstOrDefault(e => e.Id == accountId);

                var customer = _context.Customers
                    .Include(e => e.Accounts)
                    .FirstOrDefault(e => e.Id == customerId);

                CustomerId = customerId;

                account.AccountType = AccountType;
                account.Balance = Balance;
                account.Created = Created;
                _context.SaveChanges();

                return RedirectToPage("/Bank/Customer/Customer", new {customerId = CustomerId});
            }

            return Page();
        }
    }
}
