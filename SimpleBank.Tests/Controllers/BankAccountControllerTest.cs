using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleBank.Controllers;
using System.Web.Mvc;

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
    }
}
