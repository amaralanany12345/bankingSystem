namespace banking.Models
{
    public class Customer:User
    {
        public List<Account>? customerAccounts { get; set; } = new List<Account>();
        public List<AnnualDeposit>? annualDeposits { get; set; } = new List<AnnualDeposit>();
        public List<Finance>? Finances { get; set; } = new List<Finance>();
    }
}
