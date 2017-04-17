
using Microsoft.AspNet.Identity;
using SimpleBank.Models;
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
                ViewBag.TooMuch("Can't deposit more 10,000 at a time ");
                return View();
            }

            var userId = User.Identity.GetUserId();
            var bankAccount = db.BankAccountModels.Where(c => c.ApplicationUserId == userId && c.AccountName == accountName).FirstOrDefault();
            bankAccount.Balance += depositAmount;
            db.SaveChanges();
            
            return Content("you want to deposit: " + depositAmount);
        }

        public ActionResult Withdraw(int id)
        {
            ViewBag.Message = "Hello There";
            ViewBag.TheMessage = "How much would you like to withdraw today";
            return View("");
        }

        [HttpPost]
        public ActionResult Withdraw(decimal withdrawAmount, string accountName)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            var userId = User.Identity.GetUserId();
            var bankAccount = db.BankAccountModels.Where(c => c.ApplicationUserId == userId && c.AccountName == accountName).FirstOrDefault();

            if(bankAccount.Balance - withdrawAmount <100)
            {
                ViewBag.TheMessage = "can't have less than 100$ in account";
                return View();
            }
            if((withdrawAmount / bankAccount.Balance) > 0.9M)
            {
                ViewBag.TheMessage = "can't withdraw more than 90% in one time";
                return View();
            }

            bankAccount.Balance -= withdrawAmount;
            db.SaveChanges();

            return View("");
        }
    }
}