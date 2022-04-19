using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Bank.Accounts
{
    [Authorize(Roles="Admin")]
    public class EditAccountModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
