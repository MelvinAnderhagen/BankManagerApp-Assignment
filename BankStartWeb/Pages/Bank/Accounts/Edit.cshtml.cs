using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;

namespace BankStartWeb.Pages.Bank.Accounts;

[BindProperties]
public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [Required] public string AccountType { get; set; }

    [Required] public DateTime Created { get; set; }


    public void OnGet(int accountId)
    {
        Created = DateTime.Now;

        var account = _context.Accounts.FirstOrDefault(e => e.Id == accountId);

        AccountType = account.AccountType;
        Created = account.Created;
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public IActionResult OnPost(int accountId)
    {
        if (ModelState.IsValid)
        {
            var account = _context.Accounts.FirstOrDefault(e => e.Id == accountId);

            account.AccountType = AccountType;
            account.Created = Created;

            _context.SaveChanges();
            return RedirectToPage("/Bank/Accounts/Accounts");
        }

        return Page();
    }
}