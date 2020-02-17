using Antlr.Runtime.Misc;
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
        public Func<string> GetUserId; //For testing
        
        private IApplicationDbContext db;

        public TransactionController()
        {
            db = new ApplicationDbContext();           
        }

        public TransactionController(IApplicationDbContext dbContext)
        {
            db = dbContext;
        }
        public ActionResult Deposit(int id)
        {
            var bankAccount = db.BankAccounts.Where(c => c.Id == id).FirstOrDefault();
            if (bankAccount == null)
            {
                ViewBag.Error = "Account not found";
                return View();
            }

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
                return View(transaction); 
            }

            var service = new BankAccountServices(db);
            service.Deposit(transaction);

            return RedirectToAction("Details", "BankAccount", new { id = transaction.id });
        }

        public ActionResult Withdraw(int id)
        {
            var bankAccount = db.BankAccounts.Where(c => c.Id == id).FirstOrDefault();
            if (bankAccount == null)
            {
                ViewBag.Error = "Account not found";
                return View();
            }
          
            ViewBag.TheMessage = "How much would you like to withdraw today?";

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
                case ("insufficientFunds"):
                    {
                        ViewBag.TheMessage = "can't withdraw more than 90% in one time";
                        return View(transaction);
                    }
                case ("TooMuch"):
                    {
                        ViewBag.TheMessage = "can't have less than 100$ in account";
                        return View(transaction);
                    }
                case ("Not Found"):
                    {
                        ViewBag.TheMessage = "Coudlnt find entry";
                        return RedirectToAction("Details", "BankAccount", new { id = transaction.id });
                    }
                default: { break; }
            }
            return RedirectToAction("Details", "BankAccount", new { id = transaction.id });
        }
    }
}