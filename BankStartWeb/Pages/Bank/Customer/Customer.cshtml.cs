using System.ComponentModel.DataAnnotations;
using BankStartWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Bank.Customer
{
    [Authorize]
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
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public int AccountId { get; set; }

        public class AccountViewModel
        {
            public int AccountId { get; set; }
            public string AccountType { get; set; }
            [DataType(DataType.Date)]
            public DateTime Created { get; set; }
            public decimal Balance { get; set; }
        }

        public List<AccountViewModel> Accounts { get; set; }
       
        public void OnGet(int customerId)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Id == customerId);

            var account = _context.Customers
                .Include(x => x.Accounts)
                .FirstOrDefault(x => x.Id == customerId);

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

            

            Accounts = account.Accounts.Select(x => new AccountViewModel
            {
                AccountId = x.Id,
                AccountType = x.AccountType,
                Balance = x.Balance,
                Created = x.Created
            }).ToList();

            Surname = account.Surname;
            Givenname = account.Givenname;
        }
    }
}
