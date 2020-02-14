namespace SimpleBank.Models
{
    public class Transaction
    {
        public int id { get; set; }
        public string userId { get; set; }
        public string accountName { get; set; }
        public decimal amount { get; set; }

    }
}