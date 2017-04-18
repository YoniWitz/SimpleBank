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

        public void Deposit(Transaction transaction)
        {           
            var bankAccount = db.BankAccounts.Where(c => c.ApplicationUserId.Equals(transaction.userId) && c.Id == transaction.id).FirstOrDefault();
            if (bankAccount != null)
            {
                bankAccount.Balance += transaction.amount;
                db.SaveChanges();
            }
        }

        public string Withdraw(Transaction transaction)
        {
            var bankAccount = db.BankAccounts.Where(c => c.ApplicationUserId.Equals(transaction.userId) && c.Id == transaction.id).FirstOrDefault();
            if(bankAccount == null)
            {
                return "Not Found";
            }
            if (bankAccount.Balance - transaction.amount < 100)
            {
                return "TooMuch";
            }
            if ((transaction.amount / bankAccount.Balance) > 0.9M)
            {
                return "insufficientFunds";
            }

            bankAccount.Balance -= transaction.amount;
            db.SaveChanges();
            return "";
        }
    }
}