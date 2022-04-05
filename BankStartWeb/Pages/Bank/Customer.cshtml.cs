using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Bank
{
    public class CustomerModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CustomerModel(ApplicationDbContext context)
        {
            _context = context;
        }        
        public int Id { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string CountryCode { get; set; }
        public string Country { get; set; }
        public string NationalId { get; set; }
        public int TelephoneCountryCode { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }
        public int AccountId { get; set; }
        public List<Account> Accounts { get; set; }
       
        public void OnGet(int customerId)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Id == customerId);

            var accountid = _context.Accounts.FirstOrDefault(x => x.Id == AccountId);

            Id = customer.Id;
            Givenname = customer.Givenname;
            Surname = customer.Surname;
            Streetaddress = customer.Streetaddress;
            City = customer.City;
            Zipcode = customer.Zipcode;
            TelephoneCountryCode = customer.TelephoneCountryCode;
            CountryCode = customer.CountryCode;
            Telephone = customer.Telephone;
            EmailAddress = customer.EmailAddress;
            Birthday = customer.Birthday;
            Country = customer.Country;
            NationalId = customer.NationalId;   
        }
    }
}
