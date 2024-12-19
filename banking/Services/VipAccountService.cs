using banking.Interfaces;
using banking.Models;
using Microsoft.EntityFrameworkCore;

namespace banking.Services
{
    public class VipAccountService : AccountProcedureServices, IAccount
    {
        private int createAccountDeposit=> 1000000;
        private int limitedDailyDeposit => 5000000;
        private int limitedDailyWithdraw => 3000000;
        private int limitedDailyTransfer => 120000;
        private readonly CustomerService _customerService;
        private readonly AppDbContext _context;

        public VipAccountService(CustomerService customerService, AppDbContext context):base(context)
        {
            _customerService = customerService;
            _context = context;
        }

        public async Task<Account> createAccount(int customerId, int balance)
        {
            var customer = await _customerService.getCustomer(customerId);

            var newAccount = new Account();
            newAccount.balance = balance;
            if (balance < createAccountDeposit)
            {
                throw new ArgumentException("you should deposit 1000000 when you create vip account");
            }
            newAccount.customerId = customerId;
            newAccount.customer = customer;
            newAccount.accountType = Enum.AccountType.Vip;
            var random = new Random();
            long min = 10000000000000;  // 14-digit number's minimum
            long max = 99999999999999;  // 14-digit number's maximum

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
            var account=await _context.accounts.Where(a=>a.accountNumber==accountNumber).FirstOrDefaultAsync();
            if (account == null)
            {
                throw new ArgumentException("account is not found");
            }
            return account;
        }

        public async Task<Withdraw> withdraw(string accountNumber, int value)
        {
            return await Withdraw(accountNumber, value,limitedDailyWithdraw);
        }

        public async Task<Transfer> TransferToAnotherAccount(int sentAccountId, int receivedAccountId, int transferValue)
        {
            return await transfer(sentAccountId, receivedAccountId, transferValue, limitedDailyTransfer);
        }
    }
}
