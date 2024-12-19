using banking.Interfaces;
using banking.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace banking.Services
{
    public class AnnualDepositService 
    {
        private readonly AppDbContext _context;
        private int annualRepaymentPercentage => 20;

        public AnnualDepositService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AnnualDeposit> acceptAnnualDeposit(int annualDepositId)
        {
            var annualDeposit=await getAnnualDeposit(annualDepositId);
            annualDeposit.acceptAnnualDeposit=true;
            annualDeposit.AnnualDepositRequestTime=DateTime.Now;
            annualDeposit.AnnualDepositRepaymentTime=DateTime.Now.AddMonths(12);
            await _context.SaveChangesAsync();
            return annualDeposit;
        }

        public async Task DeleteAnnualDeposit(int annualDepositId)
        {
            var annualDeposit=await getAnnualDeposit(annualDepositId);
            _context.annualDeposits.Remove(annualDeposit);
            await _context.SaveChangesAsync();
        }

        public async Task<AnnualDepositCashing> deposit(int annualDepositId,int value)
        {
            var annualDeposit = await getAnnualDeposit(annualDepositId);
            if (annualDeposit.AnnualDepositRepaymentTime > DateTime.Now)
            {
                throw new ArgumentException($"you can not deposit from your annual deposit until {annualDeposit.AnnualDepositRepaymentTime}");
            }

            if (value > annualDeposit.AnnualDepositRepaymentValue)
            {
                throw new ArgumentException("your balance is not enough");
            }
            
            var newAnnualDepositCashing= new AnnualDepositCashing();
            newAnnualDepositCashing.annualDeposit=annualDeposit;
            newAnnualDepositCashing.annualDepositId=annualDepositId;
            newAnnualDepositCashing.annualDepositCashingTime=DateTime.Now;
            newAnnualDepositCashing.annualDepositCashingValue=value;
            _context.annualDepositCashing.Add(newAnnualDepositCashing);
            await _context.SaveChangesAsync();
            annualDeposit.AnnualDepositValue = annualDeposit.AnnualDepositRepaymentValue- value;
            await _context.SaveChangesAsync();
            return newAnnualDepositCashing;
        }
        public async Task<AnnualDeposit> getAnnualDeposit(int annualDepositId)
        {
            var annualDeposit=await _context.annualDeposits.Where(a=>a.id==annualDepositId).FirstOrDefaultAsync();
            if(annualDeposit==null)
            {
                throw new ArgumentException("annual deposit is not found");
            }
            return annualDeposit;
        }

        public async Task<AnnualDeposit> requestAnnualDeposit(int customerId,int AnnualDepositValue, int AnnualDepositPeriod)
        {
            var customer = await _context.customers.Where(a => a.id == customerId).FirstOrDefaultAsync();
            if (customer == null)
            {
                throw new ArgumentException("customer is not found");
            }

            var newAnnualDeposit=new AnnualDeposit();
            newAnnualDeposit.customer=customer;
            newAnnualDeposit.customerId=customerId;
            newAnnualDeposit.identityNumber=customer.identityNumber;
            newAnnualDeposit.AnnualDepositValue=AnnualDepositValue;
            newAnnualDeposit.AnnualDepositRepaymentValue = newAnnualDeposit.AnnualDepositValue + (int)(newAnnualDeposit.AnnualDepositValue * (float)(annualRepaymentPercentage) / (float)(100)); 
            newAnnualDeposit.AnnualDepositRequestTime= DateTime.Now;
            newAnnualDeposit.AnnualDepositPeriod=AnnualDepositPeriod;
            newAnnualDeposit.AnnualDepositRepaymentTime=DateTime.Now.AddMonths(AnnualDepositPeriod);
            var random = new Random();
            long min = 100000000000;
            long max = 999999999999;
            long randomDigitNumber = min + (long)(random.NextDouble() * (max - min));
            newAnnualDeposit.annualDepositNumber= randomDigitNumber.ToString();
            _context.annualDeposits.Add(newAnnualDeposit);
            await _context.SaveChangesAsync();
            return newAnnualDeposit;
        }

        public async Task<Account> TransferAnnualDepositToAccount(int customerId, int annualDepositId)
        {
            var customer=await _context.customers.Where(a=>a.id==customerId).FirstOrDefaultAsync();
            if(customer==null)
            {
                throw new ArgumentException("customer is not found");
            }
            var account=await _context.accounts.Where(a=>a.customerId == customerId).FirstOrDefaultAsync();
            if(account == null)
            {
                throw new ArgumentException("account is not found");
            }

            var annualDeposit=await _context.annualDeposits.Where(a=>a.id==annualDepositId).FirstOrDefaultAsync();
            if(annualDeposit == null)
            {
                throw new ArgumentException("annual deposit is not found");
            }

            account.balance += annualDeposit.AnnualDepositRepaymentValue;
            _context.annualDeposits.Remove(annualDeposit);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<AnnualDeposit> reNewAnnualDeposit(int annualDepositId, int annualDepositPeriod, int value)
        {
            var annualDeposit=await getAnnualDeposit(annualDepositId);
            annualDeposit.AnnualDepositValue += value;
            annualDeposit.AnnualDepositRepaymentValue = annualDeposit.AnnualDepositValue + (int)(annualDeposit.AnnualDepositValue * (float)(annualRepaymentPercentage) / (float)(100));
            annualDeposit.AnnualDepositRequestTime = DateTime.Today;
            annualDeposit.AnnualDepositPeriod =annualDepositPeriod;
            annualDeposit.AnnualDepositRepaymentTime= annualDeposit.AnnualDepositRequestTime.AddMonths(annualDepositPeriod);
            await _context.SaveChangesAsync();
            return annualDeposit;
        }
    }
}
