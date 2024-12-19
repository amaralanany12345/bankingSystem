using banking.Interfaces;
using banking.Models;
using Microsoft.EntityFrameworkCore;

namespace banking.Services
{
    public class SavingAccountService : AccountProcedureServices, IAccount
    {
        private readonly AppDbContext _context;
        private readonly CustomerService _customerService;
        private int limitedDailyDeposit => 50000;
        private int limitedDailyWithdraw => 10000;
        public SavingAccountService(AppDbContext context, CustomerService customerService) : base(context)
        {
            _context = context;
            _customerService = customerService;
        }

        public async Task<Account> createAccount(int customerId, int balance)
        {
            var customer = await _customerService.getCustomer(customerId);

            var newAccount = new Account();
            newAccount.balance = balance;
            newAccount.customerId = customerId;
            newAccount.customer = customer;
            newAccount.accountType = Enum.AccountType.savings;
            var random = new Random();
            long min = 10000000000000;  
            long max = 99999999999999;  

            long random14DigitNumber = min + (long)(random.NextDouble() * (max - min));
            newAccount.accountNumber = random14DigitNumber.ToString();
            _context.accounts.Add(newAccount);
            await _context.SaveChangesAsync();
            return newAccount;
        }

        public async Task deleteAccount(string accountNumber)
        {
            var account=await getAccount(accountNumber);
            _context.accounts.Remove(account);
            await _context.SaveChangesAsync();
        }

        public async Task<Deposit> deposit(string accountNumber, int value)
        {
            return await Deposit(accountNumber, value,limitedDailyDeposit);
        }

        public async Task<Account> getAccount(string accountNumber)
        {
            var account = await _context.accounts.Where(a => a.accountNumber == accountNumber).FirstOrDefaultAsync();
            if(account == null)
            {
                throw new ArgumentException("account is not found");
            }
            return account;
        }

        public async Task<Withdraw> withdraw(string accountNumber, int value)
        {
            return await Withdraw(accountNumber, value,limitedDailyWithdraw);
        }

    }
}
