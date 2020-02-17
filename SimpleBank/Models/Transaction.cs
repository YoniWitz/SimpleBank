using System.ComponentModel.DataAnnotations;

namespace SimpleBank.Models
{
    public class Transaction
    {
        public int id { get; set; }
        public string userId { get; set; }
        public string accountName { get; set; }
        [Display(Name = "Amount")]
        public decimal amount { get; set; }

    }
}