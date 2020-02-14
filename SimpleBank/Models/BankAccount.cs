using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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