namespace banking.Models
{
    public class Deposit
    {
        public int id { get; set; }
        public int accountId { get; set; }
        public Account account { get; set; }
        public DateTime depositTime { get; set; }
        public int valueDeposit{ get; set; }
    }
}
