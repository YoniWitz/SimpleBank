﻿using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace SimpleBank.Models
{
    public interface IApplicationDbContext
    {
        IDbSet<BankAccount> BankAccounts { get; set; }

        int SaveChanges();
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>,
        IApplicationDbContext
    {
       
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public IDbSet<BankAccount> BankAccounts { get; set; }
    }

    public class FakeApplicationDbContext : IApplicationDbContext
    {
        public IDbSet<BankAccount> BankAccounts { get; set; }

        public int SaveChanges()
        {
            return 0;
        }
    }
}