using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleBank.Models
{
    public class BankAccount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Account Name")]
        [StringLength(10)]
        [Column(TypeName = "varchar")]
        public string AccountName { get; set; }

        //[Required]
        //[Display(Name = "First Name")]
        //public string FirstName { get; set; }

        //[Required]
        //[Display(Name = "Last Name")]
        //public string LastName { get; set; }

        //public string Name
        //{
        //    get
        //    {
        //        return string.Format("{0} {1}", this.FirstName, this.LastName);
        //    }
        //}

        [DataType(DataType.Currency)]
        [Required]
        public decimal Balance { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public BankAccount() { }
        public BankAccount(string accountName, string userId)
        {
            AccountName = accountName;
            Balance = 0;
            ApplicationUserId = userId;
        }
    }
}