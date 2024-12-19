namespace banking.Models
{
    public class Finance
    {
        public int id { get; set; }
        public string FinanceNumber { get; set; }
        public Customer customer { get; set; }
        public int customerId { get; set; }
        public string identityNumber { get; set; }
        public int FinanceValue { get; set; }
        public int FinanceRepaymentValue { get; set; }
        public DateTime FinanceRequestTime { get; set; }
        public int FinancePeriod { get; set; }
        public DateTime FinanceRepaymentTime { get; set; }
        public bool isFinanceRepaid { get; set; }
        public bool acceptFinance { get; set; }
    }
}
