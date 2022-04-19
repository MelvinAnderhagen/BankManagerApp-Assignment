using System.ComponentModel.DataAnnotations;
using BankStartWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Bank.Customers
{
    [Authorize(Roles="Admin")]
    [BindProperties]
    public class EditCustomerModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public string Givenname { get; set; }
        
        public string Surname { get; set; }
        
        public string Streetaddress { get; set; }
        
        public string City { get; set; }
        
        public string Zipcode { get; set; }
        
        public string Country { get; set; }
       
        public string CountryCode { get; set; }
       
        public string NationalId { get; set; }
        
        public string Telephone { get; set; }
        
        [EmailAddress]
        public string EmailAddress { get; set; }
        
        public int TelephoneConutryCode { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        public EditCustomerModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet(int customerId)
        {
            var customer = _context.Customers.FirstOrDefault(e => e.Id == customerId);

            Givenname = customer.Givenname;
            Surname = customer.Surname;
            Streetaddress = customer.Streetaddress;
            City = customer.City;
            Zipcode = customer.Zipcode;
            Country = customer.Country;
            CountryCode = customer.CountryCode;
            NationalId = customer.NationalId;
            Telephone = customer.Telephone;
            TelephoneConutryCode = customer.TelephoneCountryCode;
            BirthDay = customer.Birthday;
            EmailAddress = customer.EmailAddress;


        }

        public IActionResult OnPost(int customerId)
        {
            if (ModelState.IsValid)
            {
                var customer = _context.Customers.FirstOrDefault(e => e.Id == customerId);

                customer.EmailAddress = EmailAddress;
                customer.Birthday = BirthDay;
                customer.Givenname = Givenname;
                customer.Surname = Surname;
                customer.Streetaddress = Streetaddress;
                customer.City = City;
                customer.Zipcode = Zipcode;
                customer.Country = Country;
                customer.CountryCode = CountryCode;
                customer.NationalId = NationalId;
                customer.Telephone = Telephone;
                customer.TelephoneCountryCode = TelephoneConutryCode;
                _context.SaveChanges();

                return RedirectToPage("/Bank/Customers/Customers");
            }

            return Page();
        }
    }
}
