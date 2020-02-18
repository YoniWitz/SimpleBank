using Microsoft.AspNet.Identity;
using SimpleBank.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SimpleBank.Controllers
{
    [Authorize]
    public class BankAccountController : Controller
    {
        private IApplicationDbContext db;

        public BankAccountController()
        {
            db = new ApplicationDbContext();
        }

        public BankAccountController(IApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        // GET: BankAccount
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var bankAccounts = db.BankAccounts.Where(c => c.ApplicationUserId == userId);
                    
            return View(bankAccounts);
        }

        // GET: BankAccount/Details
        public ActionResult Details(int id)
        {
            BankAccount bankAccount = db.BankAccounts.Where(a => a.Id == id).FirstOrDefault();
            ViewBag.Id = id;
            if (bankAccount != null)
                return View(bankAccount);
            else
                return View("Index");             
        }

        // GET: BankAccount/Details
        public ActionResult Options(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        // GET: BankAccount/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BankAccount/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            string bankAccountName = Convert.ToString(collection["AccountName"]);
            var userId = User.Identity.GetUserId();

            //make sure the user doesnt have an bank account with same name
            var bankAccount = db.BankAccounts.Where(a => a.AccountName == bankAccountName && a.ApplicationUserId.Equals(userId)).FirstOrDefault();

            if (bankAccount != null)
            {
                ViewBag.Error = "Can't use same name twice";
                return View();
            }

            try
            {             
                var bankAccountModel = new BankAccount(bankAccountName, userId);
                db.BankAccounts.Add(bankAccountModel);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BankAccount/Delete/5
        public ActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            var bankAccount = db.BankAccounts.Where(a => a.Id == id && a.ApplicationUserId.Equals(userId)).FirstOrDefault();
            if (bankAccount != null)
            {
                ViewBag.Id = id;
                return View(bankAccount);
            }
            return View("Options");
        }

        // POST: BankAccount/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var userId = User.Identity.GetUserId();
            try
            {
                var bankAccount = db.BankAccounts.Where(a => a.Id == id).FirstOrDefault();
                if (bankAccount != null)
                {
                    db.BankAccounts.Remove(bankAccount);
                    db.SaveChanges();
                }
                var bankAccounts = db.BankAccounts.Where(c => c.ApplicationUserId == userId);

                return View("Index", bankAccounts);
            }
            catch
            {
                return View();
            }
        }
    }
}
