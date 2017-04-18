using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleBank.Models;
using SimpleBank.Controllers;
using System.Web.Mvc;

namespace SimpleBank.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CantDepositMoreThan10000()
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

            fakeDb.BankAccounts.Add(bankAccount);

            var transactionController = new TransactionController(fakeDb)
            {
                GetUserId = () => "1"
            };
            var result = transactionController.Deposit(1) as ViewResult;

            Assert.AreEqual(1, result.ViewBag.Id);
        }
    }
}
