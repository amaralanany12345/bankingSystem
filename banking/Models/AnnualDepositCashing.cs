namespace banking.Models
{
    public class AnnualDepositCashing
    {
        public int id { get; set; }
        public int annualDepositId { get; set; }
        public AnnualDeposit annualDeposit { get; set; }
        public DateTime annualDepositCashingTime { get; set; }
        public int annualDepositCashingValue { get; set; }
    }
}
