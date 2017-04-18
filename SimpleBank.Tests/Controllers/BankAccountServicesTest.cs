using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleBank.Models;
using SimpleBank.Services.BankAccountServices;

namespace SimpleBank.Tests.Controllers
{
    [TestClass]
    public class BankAccountServicesTest
    {
        [TestMethod]
        public void AddingAmmountSaves()
        {
            var fakeDb = new FakeApplicationDbContext();
            fakeDb.BankAccounts = new FakeDbSet<BankAccount>();

            var bankAccount = new BankAccount()
            {
                Id = 1,
                AccountName = "new",
                Balance = 0,
                ApplicationUserId = "1"
            };

            var transaction = new Transaction() { amount = 100, id = 1, userId = "1" };
            fakeDb.BankAccounts.Add(bankAccount);

            var bankAccountServices = new BankAccountServices(fakeDb) { };
            
            bankAccountServices.Deposit(transaction);

            Assert.AreEqual(bankAccount.Balance, 100);
        }

        [TestMethod]
        public void CanWithdraw()
        {
            var fakeDb = new FakeApplicationDbContext();
            fakeDb.BankAccounts = new FakeDbSet<BankAccount>();

            var bankAccount = new BankAccount()
            {
                Id = 1,
                AccountName = "new",
                Balance = 1000,
                ApplicationUserId = "1"
            };

            var transaction = new Transaction() { amount = 1, id = 1, userId = "1" };
            fakeDb.BankAccounts.Add(bankAccount);

            var bankAccountServices = new BankAccountServices(fakeDb) { };

            var result = bankAccountServices.Withdraw(transaction);

            Assert.AreEqual(bankAccount.Balance, 999);
        }

        [TestMethod]
        public void CantWithdrawAndHaveLess100()
        {
            var fakeDb = new FakeApplicationDbContext();
            fakeDb.BankAccounts = new FakeDbSet<BankAccount>();

            var bankAccount = new BankAccount()
            {
                Id = 1,
                AccountName = "new",
                Balance = 100,
                ApplicationUserId = "1"
            };

            var transaction = new Transaction() { amount = 1, id = 1, userId = "1" };
            fakeDb.BankAccounts.Add(bankAccount);

            var bankAccountServices = new BankAccountServices(fakeDb) { };

            var result = bankAccountServices.Withdraw(transaction);

            Assert.AreEqual(result, "TooMuch");
        }

        [TestMethod]
        public void CantWithdrawMoreThanNinetyPercent()
        {
            var fakeDb = new FakeApplicationDbContext();
            fakeDb.BankAccounts = new FakeDbSet<BankAccount>();

            var bankAccount = new BankAccount()
            {
                Id = 1,
                AccountName = "new",
                Balance = 2000,
                ApplicationUserId = "1"
            };

            var transaction = new Transaction() { amount = 1801, id = 1, userId = "1" };
            fakeDb.BankAccounts.Add(bankAccount);

            var bankAccountServices = new BankAccountServices(fakeDb) { };

            var result = bankAccountServices.Withdraw(transaction);

            Assert.AreEqual(result, "insufficientFunds");
        }
    }
}
