namespace banking.Dto
{
    public class FinanceDto
    {
        public int id { get; set; }
        public int accountId { get; set; }
        public int FinanceValue { get; set; }
        public int FinanceRepaymentValue { get; set; }
        public DateTime FinanceRequestTime { get; set; }
        public int FinancePeriod { get; set; }
        public DateTime FinanceRepaymentTime { get; set; }
        public bool acceptFinance { get; set; }
    }
}
