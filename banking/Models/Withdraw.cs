namespace banking.Models
{
    public class Withdraw
    {
        public int id { get;set; }
        public int accountId { get;set; }
        public Account account { get;set; } 
        public DateTime withdrawTime { get;set; }
        public int valueWithdraw { get;set; }
    }
}
