using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BankStartWeb.Data;
using BankStartWeb.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankTest;

[TestClass]
public class AccountServiceTests
{
    private readonly ApplicationDbContext _context;
    private readonly AccountService _sut;

    public AccountServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Test")
            .Options;

        _context = new ApplicationDbContext(options);
        _sut = new AccountService(_context);
    }

    [TestMethod]
    public void When_Deposit_Is_Negative_Balance()
    {
        _context.Accounts.Add(new Account
        {
            Balance = 10000,
            AccountType = "Savings",
            Created = DateTime.Now,
            Transactions = new List<Transaction>()
        });

        _context.Customers.Add(new Customer
        {
            Givenname = "Lars",
            Surname = "Karlsson",
            Streetaddress = "Grönfiksvägen 42",
            City = "Stockholm",
            Country = "Sweden",
            Zipcode = "14261",
            Telephone = "01231232",
            CountryCode = "23",
            NationalId = "1231234",
            TelephoneCountryCode = 4213,
            EmailAddress = "lars@karlsson.se",
            Birthday = DateTime.Now,
            Accounts = new List<Account>()
        });

        _context.SaveChanges();

        var result = _sut.Deposit(1, -1000);

        //var accountId = _context.Accounts
        //    .FirstOrDefault(e => e.Id == 1);


        Assert.AreEqual(IAccountService.ErrorCode.AmountIsNegative, result);
    }

    [TestMethod]
    public void When_Account_Balance_Is_To_Low_Withdraw()
    {
        _context.Accounts.Add(new Account
        {
            Balance = 3000,
            AccountType = "Savings",
            Created = DateTime.Now,
            Transactions = new List<Transaction>()
        });

        _context.Customers.Add(new Customer
        {
            Givenname = "Lars",
            Surname = "Karlsson",
            Streetaddress = "Grönfiksvägen 42",
            City = "Stockholm",
            Country = "Sweden",
            Zipcode = "14261",
            Telephone = "01231232",
            CountryCode = "23",
            NationalId = "1231234",
            TelephoneCountryCode = 4213,
            EmailAddress = "lars@karlsson.se",
            Birthday = DateTime.Now,
            Accounts = new List<Account>()
        });

        _context.SaveChanges();

        var result = _sut.WithDraw(1, 4000);

        //var accountId = _context.Accounts
        //    .FirstOrDefault(e => e.Id == 1);


        Assert.AreEqual(IAccountService.ErrorCode.BalanceIsToLow, result);

    }

    [TestMethod]
    public void When_Account_Balance_Is_To_Low_Transfer()
    {
        _context.Accounts.Add(new Account
        {
            Balance = 5000,
            AccountType = "Savings",
            Created = DateTime.Now,
            Transactions = new List<Transaction>()
        });

        _context.Accounts.Add(new Account
        {
            Balance = 1000,
            AccountType = "Personal",
            Created = DateTime.Now,
            Transactions = new List<Transaction>()
        });

        _context.Customers.Add(new Customer
        {
            Givenname = "Lars",
            Surname = "Karlsson",
            Streetaddress = "Grönfiksvägen 42",
            City = "Stockholm",
            Country = "Sweden",
            Zipcode = "14261",
            Telephone = "01231232",
            CountryCode = "23",
            NationalId = "1231234",
            TelephoneCountryCode = 4213,
            EmailAddress = "lars@karlsson.se",
            Birthday = DateTime.Now,
            Accounts = new List<Account>()
        });

        _context.SaveChanges();

        //var receiverId = _context.Accounts
        //    .FirstOrDefault(e => e.Id == 2);

        var result = _sut.Transfer(1, 6000, 2);

        Debug.WriteLine(result);

        Assert.AreEqual(IAccountService.ErrorCode.BalanceIsToLow, result);
    }

    [TestMethod]
    public void When_Account_Amount_Is_Negative_Transfer()
    {
        _context.Accounts.Add(new Account
        {
            Balance = 5000,
            AccountType = "Savings",
            Created = DateTime.Now,
            Transactions = new List<Transaction>()
        });

        _context.Accounts.Add(new Account
        {
            Balance = 1000,
            AccountType = "Personal",
            Created = DateTime.Now,
            Transactions = new List<Transaction>()
        });

        _context.Customers.Add(new Customer
        {
            Givenname = "Lars",
            Surname = "Karlsson",
            Streetaddress = "Grönfiksvägen 42",
            City = "Stockholm",
            Country = "Sweden",
            Zipcode = "14261",
            Telephone = "01231232",
            CountryCode = "23",
            NationalId = "1231234",
            TelephoneCountryCode = 4213,
            EmailAddress = "lars@karlsson.se",
            Birthday = DateTime.Now,
            Accounts = new List<Account>()
        });

        _context.SaveChanges();

        //var receiverId = _context.Accounts
        //    .FirstOrDefault(e => e.Id == 2);

        var result = _sut.Transfer(1, -5000, 2);

        Debug.WriteLine(result);

        Assert.AreEqual(IAccountService.ErrorCode.AmountIsNegative, result);
    }


}