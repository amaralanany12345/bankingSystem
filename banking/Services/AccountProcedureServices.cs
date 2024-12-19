using banking.Models;
using Microsoft.EntityFrameworkCore;

namespace banking.Services
{
    public class AccountProcedureServices
    {
        private readonly AppDbContext _context;

        public AccountProcedureServices(AppDbContext context)
        {
            _context = context;
        }
        protected async Task<Deposit> Deposit(string accountNumber, int value, int limitedDepositInDay)
        {
            var account = await _context.accounts.Where(a=>a.accountNumber==accountNumber).FirstOrDefaultAsync();
            if (account == null)
            {
                throw new ArgumentException("account is not found");
            }
            
            //if(account.isActive == false)
            //{
            //    throw new ArgumentException("your account is not active so you can't deposit from your account");
            //}

            var DepositProcess = await _context.deposits.Where(a => a.accountId == account.id && a.depositTime == DateTime.Today).ToListAsync();
            var totalDepositInDay = 0;
            foreach (var item in DepositProcess)
            {
                totalDepositInDay += item.valueDeposit;
            }

            if(value>limitedDepositInDay || (totalDepositInDay+value) > limitedDepositInDay)
            {
                throw new ArgumentException("you are reach to the maximum limit to deposit in day");
            }
            var newDeposit=new Deposit();
            newDeposit.valueDeposit=value;
            newDeposit.depositTime=DateTime.Today;
            newDeposit.account=account;
            newDeposit.accountId=account.id;
            _context.deposits.Add(newDeposit);
            await _context.SaveChangesAsync();
            account.balance += value;
            await _context.SaveChangesAsync();
            return newDeposit;

        }

        protected async Task<Withdraw> Withdraw(string accountNumber,int value,int limitedWithdrawInDay)
        {

            var account=await _context.accounts.Where(a=>a.accountNumber==accountNumber).FirstOrDefaultAsync();
            if (account == null)
            {
                throw new ArgumentException("account is not found");
            }

            if (account.isActive == false)
            {
                throw new ArgumentException("your account is not active so you can't withdraw from your account");
            }

            if (value > account.balance)
            {
                throw new ArgumentException("your balance is not enough");
            }

            var withdrawProcess=await _context.withdraws.Where(a=>a.accountId==account.id && a.withdrawTime==DateTime.Today).ToListAsync();
            var totalWithdrawInDay = 0;
            foreach(var item in withdrawProcess)
            {
                totalWithdrawInDay += item.valueWithdraw;
            }
            if(value>limitedWithdrawInDay || (totalWithdrawInDay + value) >limitedWithdrawInDay)
            {
                throw new ArgumentException("you can't withDraw more");
            }
            var newWithdraw = new Withdraw();
            newWithdraw.valueWithdraw = value;
            newWithdraw.withdrawTime=DateTime.Today;
            newWithdraw.account= account;
            newWithdraw.accountId=account.id;

            _context.withdraws.Add(newWithdraw);
            await _context.SaveChangesAsync();

            account.balance -= value;
            await _context.SaveChangesAsync();

            return newWithdraw;
        }

        protected async Task<Transfer> transfer(int sentAccountId,int receivedAccountId, int transferValue,int limitedDailyTransfer)
        {
            //var sentAccount=await _context.accounts.Where(a=>a.id==sentAccountId).FirstOrDefaultAsync();
            //if(sentAccount==null)
            //{
            //    throw new ArgumentException("sent account is not found");
            //}

            //var receivedAccount = await _context.accounts.Where(a => a.id == receivedAccountId).FirstOrDefaultAsync();
            //if (receivedAccount == null)
            //{
            //    throw new ArgumentException("received account is not found");
            //}
            //var totalTransferInDay = 0;
            //var dailyProcess = await _context.transfers.Where(a => a.sentAccountId == sentAccountId && a.transferTime==DateTime.Today).ToListAsync();
            //foreach(var item in dailyProcess)
            //{
            //    totalTransferInDay += item.transferValue;
            //}
            //if (transferValue > sentAccount.balance)
            //{
            //    throw new ArgumentException("your balance is not enough");
            //}

            //if (totalTransferInDay > limitedDailyTransfer || (totalTransferInDay + transferValue)> limitedDailyTransfer)
            //{
            //    throw new ArgumentException("you are reach to your limit today");
            //}

            //var newTransfer=new Transfer();
            //newTransfer.sentAccount=sentAccount;
            //newTransfer.sentAccountId=sentAccountId;
            //newTransfer.receivedAccount = receivedAccount;
            //newTransfer.receivedAccountId=receivedAccountId;
            //newTransfer.transferTime = DateTime.Today;
            //newTransfer.transferValue = transferValue;
            //_context.transfers.Add(newTransfer);
            //await _context.SaveChangesAsync();

            //sentAccount.balance -=transferValue;
            //await _context.SaveChangesAsync();

            //receivedAccount.balance += transferValue;
            //await _context.SaveChangesAsync();

            //return newTransfer;
            throw new ArgumentException("");
        }
    }
}
