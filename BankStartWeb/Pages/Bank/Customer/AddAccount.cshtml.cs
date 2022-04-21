using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BankStartWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Bank.Customer
{
    [Authorize(Roles = "Admin")]
    [BindProperties]
    public class AddAccountModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        public AddAccountModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int CustomerId { get; set; }
        
        [BindProperty]
        public string AccountType { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        public List<SelectListItem> AllAccountTypes { get; set; } = new List<SelectListItem>();

        public void OnGet(int customerId)
        {
            Created = DateTime.Now;

            var customer = _context.Customers
                .FirstOrDefault(e => e.Id == customerId);

            CustomerId = customerId;

            AllAccountTypes.Add(new SelectListItem("Savings", "Savings"));
            AllAccountTypes.Add(new SelectListItem("Checking", "Checking"));
            AllAccountTypes.Add(new SelectListItem("Personal", "Personal"));

        }

        public IActionResult OnPost(int customerId)
        {
            var customer = _context.Customers
                .Include(e => e.Accounts)
                .FirstOrDefault(e => e.Id == customerId);

            CustomerId = customerId;

            if (ModelState.IsValid)
            {
                var account = new Data.Account();
                
                account.AccountType = AccountType;
                account.Balance = 0;
                account.Created = Created;
                customer.Accounts.Add(account);
                _context.SaveChanges();

                return RedirectToPage("/Bank/Customer/Customer", new {customerId = CustomerId});
            }

            return Page();

        }
    }
}
