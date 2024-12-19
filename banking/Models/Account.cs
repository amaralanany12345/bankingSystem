using banking.Enum;

namespace banking.Models
{
    public class Account
    {
        public int id { get; set; } 
        public string accountNumber { get; set; }
        public Customer customer { get; set; }
        public int customerId { get; set; }
        public int balance { get; set; }
        public AccountType accountType { get; set; }
        public bool isActive { get; set; }
        public List<Withdraw> withdrawProcesses { get; set; }= new List<Withdraw>();
        public List<Deposit> depositProcesses { get; set; }= new List<Deposit>();
        public List<Transfer> transferToProcesses { get; set; }= new List<Transfer>();
        public List<Transfer> transferFromProcesses { get; set; }= new List<Transfer>();
    }
}
