using Microsoft.AspNet.Identity;
using SimpleBank.Models;
using SimpleBank.Services.BankAccountServices;
using System.Linq;
using System.Web.Mvc;

namespace SimpleBank.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       
        public ActionResult Deposit(int id)
        {
            ViewBag.Message = "Hello There";
            ViewBag.TheMessage = "How much would you like to Deposit today";
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public ActionResult Deposit(decimal depositAmount, string accountName)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (depositAmount > 10000)
            {
                ViewBag.TooMuch = "Can't deposit more 10,000$ at a time ";
                return View();
            }

            var userId = User.Identity.GetUserId();
            var service = new BankAccountServices(db);
            service.UpdateBalance(depositAmount, userId, accountName);

            return Content("you want to deposit: " + depositAmount);
        }

        public ActionResult Withdraw(int id)
        {
            ViewBag.Message = "Hello There";
            ViewBag.TheMessage = "How much would you like to withdraw today";
            return View();
        }

        [HttpPost]
        public ActionResult Withdraw(decimal withdrawAmount, string accountName)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var userId = User.Identity.GetUserId();
            
            var service = new BankAccountServices(db);
            
            string result = service.Withdraw(withdrawAmount, userId, accountName);
            switch (result)
            {
                case ("TooMuch"):
                {
                        ViewBag.TheMessage = "can't withdraw more than 90% in one time";
                        return View();
                }
                case ("insufficientFunds"):
                    {
                        ViewBag.TheMessage = "can't have less than 100$ in account";
                        return View();
                    }
                default: { break; }
            }
            
            return View("");
        }
    }
}