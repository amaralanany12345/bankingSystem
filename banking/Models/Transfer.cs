namespace banking.Models
{
    public class Transfer
    {
        //public int transferId { get; set; }
        public int id { get; set; }
        public int sentAccountId { get; set; }
        public Account sentAccount { get; set; }
        public int receivedAccountId { get; set; }
        public Account receivedAccount { get; set; }
        public int transferValue { get; set; }
        public DateTime transferTime { get; set; }

    }
}
