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
            var bankAccount = db.BankAccounts.Where(c => c.Id == id).FirstOrDefault();
            ViewBag.Message = "Hello There";
            ViewBag.TheMessage = "How much would you like to Deposit today";

            Transaction transaction = new Transaction();
            transaction.id = id;
            transaction.accountName = bankAccount.AccountName;
            transaction.userId = User.Identity.GetUserId();

            return View(transaction);
        }

        [HttpPost]
        public ActionResult Deposit(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (transaction.amount > 10000)
            {
                ViewBag.TooMuch = "Can't deposit more 10,000$ at a time ";
                return View();
            }
            
            var service = new BankAccountServices(db);
            service.UpdateBalance(transaction);

            return RedirectToAction("Details", "BankAccount", new { id = transaction.id });
        }

        public ActionResult Withdraw(int id)
        {
            ViewBag.Message = "Hello There";
            ViewBag.TheMessage = "How much would you like to withdraw today";

            var bankAccount = db.BankAccounts.Where(c => c.Id == id).FirstOrDefault();

            Transaction transaction = new Transaction();
            transaction.id = id;
            transaction.accountName = bankAccount.AccountName;
            transaction.userId = User.Identity.GetUserId();

            return View(transaction);
        }

        [HttpPost]
        public ActionResult Withdraw(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            var service = new BankAccountServices(db);
            
            string result = service.Withdraw(transaction);
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
            return RedirectToAction("Details", "BankAccount", new { id = transaction.id });
        }
    }
}