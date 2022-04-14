using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankStartWeb.Pages.Bank.Customers
{
    [BindProperties]
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

        public NewCustomerModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            BirthDay = DateTime.Now;
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
