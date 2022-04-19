using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankStartWeb.Pages.Bank.Customers
{
    [BindProperties]
    [Authorize(Roles = "Admin")]
    public class NewCustomerModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        
        [Required]
        public string Givenname { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Streetaddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Zipcode { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string CountryCode { get; set; }
        [Required]
        public string NationalId { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required] 
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required] 
        public int TelephoneConutryCode { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        public List<SelectListItem> AllCountries { get; set; } = new List<SelectListItem>();

        public List<SelectListItem> AllCountryCodes { get; set; } = new List<SelectListItem>();

        public List<SelectListItem> AllTelephoneCountryCodes { get; set; } = new List<SelectListItem>();

        public NewCustomerModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            BirthDay = DateTime.Now;

            AllCountries.Add( new SelectListItem("Finland", "Finland"));
            AllCountries.Add(new SelectListItem("Sweden", "Sweden"));
            AllCountries.Add(new SelectListItem("Norway", "Norway"));
            AllCountries.Add(new SelectListItem("Denmark", "Denmark"));

            AllTelephoneCountryCodes.Add(new SelectListItem("Finland(+358)", "+358"));
            AllTelephoneCountryCodes.Add(new SelectListItem("Sweden(+46)", "+46"));
            AllTelephoneCountryCodes.Add(new SelectListItem("Norway(+47)", "+47"));
            AllTelephoneCountryCodes.Add(new SelectListItem("Denmark(+45)", "+45"));

            AllCountryCodes.Add(new SelectListItem("FI", "FI"));
            AllCountryCodes.Add(new SelectListItem("SE", "SE"));
            AllCountryCodes.Add(new SelectListItem("NO", "NO"));
            AllCountryCodes.Add(new SelectListItem("DK", "DK"));

        }
        
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var customer = new Data.Customer();
                customer.Givenname = Givenname;
                customer.Surname = Surname; 
                customer.Streetaddress = Streetaddress;
                customer.City = City;
                customer.Zipcode = Zipcode;
                customer.Country = Country;
                customer.EmailAddress = EmailAddress;
                customer.NationalId = NationalId;
                customer.Telephone = Telephone;
                customer.CountryCode = CountryCode;
                customer.Birthday = BirthDay;
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return RedirectToPage("/Bank/Customers/Customers");
            }

            return Page();
        }


    }
}
