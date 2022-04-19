using System.ComponentModel.DataAnnotations;
using BankStartWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Bank.Accounts
{
    [Authorize(Roles = "Admin")]
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
        public void OnGet(int customerId)
        {
            Created = DateTime.Now;

            var customer = _context.Customers
                .FirstOrDefault(e => e.Id == customerId);


        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var account = new Account
                {
                    Balance = 0,
                    AccountType = AccountType,
                    Created = Created
                };
                _context.SaveChanges();

                return RedirectToPage("/Bank/Accounts/Accounts");
            }

            return Page();

        }
    }
}
