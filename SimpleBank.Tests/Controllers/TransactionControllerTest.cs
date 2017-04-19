using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleBank.Models;
using SimpleBank.Controllers;
using System.Web.Mvc;

namespace SimpleBank.Tests.Controllers
{
    [TestClass]
    public class TransactionControllerTest
    {
        [TestMethod]
        public void CantDepositMoreThan10000()
        {
            var transaction = new Transaction() { amount = 10001 };
            var transactionController = new TransactionController();
            var result = transactionController.Deposit(transaction) as ViewResult;

            Assert.IsNotNull(result.ViewBag.TooMuch);
        }
    }
}
