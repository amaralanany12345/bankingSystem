using banking.Enum;

namespace banking.Models
{
    public class AnnualDeposit
    {
        public int id { get; set; }
        public string annualDepositNumber { get; set; }
        public Customer customer { get; set; }
        public int customerId { get; set; }
        public string identityNumber { get; set; }
        public int AnnualDepositValue { get; set; }
        public int AnnualDepositRepaymentValue { get; set; }
        public DateTime AnnualDepositRequestTime { get; set; }
        public int AnnualDepositPeriod { get; set; }
        public DateTime AnnualDepositRepaymentTime { get; set; }
        public bool acceptAnnualDeposit { get; set; }
        public AnnualDepositType annualDepositType{ get; set; }
        public int allowableCashingFromAnnualDeposit { get; set; }
        public List<AnnualDepositCashing> annualDepositCashing { get; set; }=new List<AnnualDepositCashing>();
    }
}
