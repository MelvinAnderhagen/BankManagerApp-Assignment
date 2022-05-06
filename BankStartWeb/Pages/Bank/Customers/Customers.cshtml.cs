using BankStartWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Bank.Customers
{
    [Authorize(Roles = "Admin, Cashier")]
    public class CustomersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CustomersModel(ApplicationDbContext context)
        {
            _context = context;
        }
        


        public List<CustomersViewModel> customers { get; set; }
        public string SearchString { get; set; }
        public int PageNo { get; set; }
        public string SortOrder { get; set; }
        public string SortCol { get; set; }
        public class CustomersViewModel
        {
            public int Id { get; set; }
            public string Givenname { get; set; }
            public string Surname { get; set; }
            public string Streetaddress { get; set; }
            public string City { get; set; }

        }
        public void OnGet(string searchString, int pageno = 1, string col = "id", string order = "asc")
        {
            PageNo = pageno;
            SortOrder = order;
            SortCol = col;
            SearchString = searchString;

            var sort = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                sort = sort.Where(x => x.Givenname.ToLower().Contains(searchString) 
                || x.Surname.ToLower().Contains(searchString)
                || x.Streetaddress.ToLower().Contains(searchString)
                || x.City.ToLower().Contains(searchString));
            }

            if (col == "givenname")
            {
                if (order == "asc")
                    sort = sort.OrderBy(n => n.Givenname);
                else
                    sort = sort.OrderByDescending(n => n.Givenname);

            }
            else if (col == "surname")
            {
                if (order == "asc")

                    sort = sort.OrderBy(n => n.Surname);

                else

                    sort = sort.OrderByDescending(n => n.Surname);

            }
            else if (col == "street")
            {
                if (order == "asc")
                    sort = sort.OrderBy(n => n.Streetaddress);
                else
                    sort = sort.OrderByDescending(n => n.Streetaddress);
            }
            else if(col == "city")
            {
                if (order == "asc")
                {
                    sort = sort.OrderBy(n => n.City);
                }
                else
                {
                    sort = sort.OrderByDescending(n => n.City);
                }
            }
            else if(col == "id")
            {
                if (order == "asc")
                {
                    sort = sort.OrderBy(n => n.Id);
                }
                else
                {
                    sort = sort.OrderByDescending(n => n.Id);
                }
            }

            int toSkip = (pageno - 1) * 20;
            sort = sort.Skip(toSkip).Take(20);

            customers = sort.Select(n => new CustomersViewModel
            {
                Id = n.Id,
                Givenname = n.Givenname,
                Surname = n.Surname,
                Streetaddress = n.Streetaddress,
                City = n.City
            }).ToList();
        }
    }
}
