using Microsoft.AspNet.Identity;
using SimpleBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBank.Controllers
{
    [Authorize]
    public class BankAccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BankAccount
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var bankAccounts = db.BankAccountModels.Where(c => c.ApplicationUserId == userId);
                    
            return View(bankAccounts);
        }

        // GET: BankAccount/Details
        public ActionResult Details(int id)
        {
            BankAccountModels account = db.BankAccountModels.Where(a => a.Id == id).FirstOrDefault();
            ViewBag.Id = id;
            if (account != null)
                return View(account);
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
            string accountName = Convert.ToString(collection["AccountName"]);
            var userId = User.Identity.GetUserId();

            var account = db.BankAccountModels.Where(a => a.AccountName == accountName).FirstOrDefault();

            if (account != null && account.ApplicationUserId == userId)
            {
                //add error message
                return View();
            }

            try
            {             
                var name = User.Identity.GetUserName().Split(null);

                var bankAccountModel = new BankAccountModels(accountName, name, userId);
                db.BankAccountModels.Add(bankAccountModel);
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
            return View();
        }

        // POST: BankAccount/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var account = db.BankAccountModels.Where(a => a.Id == id).FirstOrDefault();
                if (account != null)
                {
                    db.BankAccountModels.Remove(account);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
