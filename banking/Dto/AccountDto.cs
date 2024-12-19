using banking.Enum;
using banking.Models;

namespace banking.Dto
{
    public class AccountDto
    {
        public string accountNumber { get; set; }
        public Customer customer { get; set; }
        public int customerId { get; set; }
        public int balance { get; set; }
        public AccountType accountType { get; set; }
    }
}
