using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBank.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Deposit()
        {
            ViewBag.Message = "Hello There";
            ViewBag.TheMessage = "How much would you like to Deposit today";
            return View();
        }

        [HttpPost]
        public ActionResult Deposit(double depositAmount)
        {
            return Content("you want to deposit: " + depositAmount);
        }

        public ActionResult Withdraw()
        {
            ViewBag.Message = "Hello There";
            ViewBag.TheMessage = "How much would you like to withdraw today";
            return View();
        }

        [HttpPost]
        public ActionResult Withdraw(double withdrawAmount)
        {
            return Content("you want to withdraw: " + depositAmount);
        }
    }
}