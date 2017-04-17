using Microsoft.AspNet.Identity;
using SimpleBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBank.Services.BankAccountServices
{
     public class BankAccountServices
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public BankAccountServices(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void UpdateBalance(decimal amount, string userId, string accountName)
        {           
            var bankAccount = db.BankAccounts.Where(c => c.ApplicationUserId.Equals(userId) && c.AccountName.Equals(accountName)).FirstOrDefault();
            bankAccount.Balance += amount;
            db.SaveChanges();
        }
        public string Withdraw(decimal amount, string userId, string accountName)
        {
            var bankAccount = db.BankAccounts.Where(c => c.ApplicationUserId == userId && c.AccountName == accountName).FirstOrDefault();

            if (bankAccount.Balance - amount < 100)
            {
                return "TooMuch";
            }
            if ((amount / bankAccount.Balance) > 0.9M)
            {
                return "insufficientFunds";
            }

            bankAccount.Balance -= amount;
            db.SaveChanges();
            return "";
        }
    }
}