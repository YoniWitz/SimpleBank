using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleBank.Controllers;
using System.Web.Mvc;
using SimpleBank.Models;

namespace SimpleBank.Tests.Controllers
{
    [TestClass]
    public class BankAccountControllerTest
    {
        [TestMethod]
        public void OptionsGetsId()
        {
            var bankAccountController = new BankAccountController();
            var result = bankAccountController.Options(1) as ViewResult;

            Assert.AreEqual(1, result.ViewBag.Id);
        }
        [TestMethod]
        public void CantCreateSameUserSameAccountName()
        {
            var fakeDb = new FakeApplicationDbContext();
            fakeDb.BankAccounts = new FakeDbSet<BankAccount>();

            var bankAccount = new BankAccount()
            {
                Id = 1,
                AccountName = "new",
                Balance = 0,
                ApplicationUserId = "1"
            };

            FormCollection collection = new FormCollection { { "AccountName" , "new" } };
            fakeDb.BankAccounts.Add(bankAccount);

            var bankAccountController = new BankAccountController(fakeDb) { };
            
        }
    }
}
