using banking.Enum;
using banking.Interfaces;
using banking.Migrations;
using banking.Models;
using Microsoft.EntityFrameworkCore;

namespace banking.Services
{
    public class AnnualDepositWithThreeYearsService : IAnnualDeposit
    {
        private readonly AppDbContext _context;
        private int annualRepaymentPercentage => 30;

        public AnnualDepositWithThreeYearsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AnnualDeposit> acceptAnnualDeposit(int annualDepositId)
        {
            var annualDeposit = await getAnnualDeposit(annualDepositId);
            annualDeposit.acceptAnnualDeposit = true;
            annualDeposit.AnnualDepositRequestTime = DateTime.Today;
            annualDeposit.AnnualDepositRepaymentTime = annualDeposit.AnnualDepositRequestTime.AddMonths(annualDeposit.AnnualDepositPeriod);
            await _context.SaveChangesAsync();
            await transferTheReturnToAccount(annualDepositId);
            await _context.SaveChangesAsync();
            return annualDeposit;
        }

        public async Task DeleteAnnualDeposit(int annualDepositId)
        {
            var annualDeposit = await getAnnualDeposit(annualDepositId);
            _context.annualDeposits.Remove(annualDeposit);
            await _context.SaveChangesAsync();
        }

        public async Task<AnnualDepositCashing> deposit(int annualDepositId, int value)
        {
            var annualDeposit = await getAnnualDeposit(annualDepositId);
            if (DateTime.Today < annualDeposit.AnnualDepositRepaymentTime)
            {
                throw new ArgumentException($"you can't deposit the annualDeposit until {annualDeposit.AnnualDepositRepaymentTime}");
            }

            var annualDepositCashing = new AnnualDepositCashing();
            annualDepositCashing.annualDepositId = annualDepositId;
            annualDepositCashing.annualDeposit = annualDeposit;
            annualDepositCashing.annualDepositCashingValue = value;
            annualDepositCashing.annualDepositCashingTime = DateTime.Now;
            _context.annualDepositCashing.Add(annualDepositCashing);
            await _context.SaveChangesAsync();
            annualDeposit.AnnualDepositValue -= value;
            await _context.SaveChangesAsync();
            return annualDepositCashing;
        }

        public async Task<AnnualDeposit> getAnnualDeposit(int annualDepositId)
        {
            var annualDeposit = await _context.annualDeposits.Where(a => a.id == annualDepositId).FirstOrDefaultAsync();
            if (annualDeposit == null)
            {
                throw new ArgumentException("annual deposit is not found");
            }
            return annualDeposit;
        }

        public async Task<AnnualDeposit> reNewAnnualDeposit(int annualDepositId, int annualDepositPeriod, int value,AnnualDepositType annualDepositType)
        {
            var annualDeposit = await getAnnualDeposit(annualDepositId);
            annualDeposit.AnnualDepositValue += value;
            annualDeposit.AnnualDepositRepaymentValue = annualDeposit.AnnualDepositValue + (int)(annualDeposit.AnnualDepositValue * (float)(annualRepaymentPercentage) / (float)(100));
            annualDeposit.AnnualDepositRequestTime = DateTime.Today;
            annualDeposit.AnnualDepositPeriod = annualDepositPeriod;
            annualDeposit.AnnualDepositRepaymentTime = annualDeposit.AnnualDepositRequestTime.AddMonths(annualDepositPeriod);
            annualDeposit.annualDepositType = annualDepositType;
            await _context.SaveChangesAsync();
            return annualDeposit;
        }

        public async  Task<AnnualDeposit> requestAnnualDeposit(int customerId, int AnnualDepositValue)
        {
            var customer = await _context.customers.Where(a => a.id == customerId).FirstOrDefaultAsync();
            if (customer == null)
            {
                throw new ArgumentException("customer is not found");
            }

            var newAnnualDeposit = new AnnualDeposit();
            newAnnualDeposit.customer = customer;
            newAnnualDeposit.customerId = customerId;
            newAnnualDeposit.identityNumber = customer.identityNumber;
            newAnnualDeposit.AnnualDepositValue = AnnualDepositValue;
            newAnnualDeposit.AnnualDepositPeriod = 36;
            newAnnualDeposit.AnnualDepositRepaymentValue = newAnnualDeposit.AnnualDepositValue + (int)(newAnnualDeposit.AnnualDepositValue * (float)(annualRepaymentPercentage) / (float)(100) * newAnnualDeposit.AnnualDepositPeriod/12);
            newAnnualDeposit.AnnualDepositRequestTime = DateTime.Today;
            //newAnnualDeposit.AnnualDepositRequestTime = new DateTime(2021,10,17);
            newAnnualDeposit.AnnualDepositRepaymentTime = newAnnualDeposit.AnnualDepositRequestTime.AddMonths(newAnnualDeposit.AnnualDepositPeriod);
            newAnnualDeposit.annualDepositType = AnnualDepositType.threeYears;
            var random = new Random();
            long min = 100000000000;
            long max = 999999999999;
            long randomDigitNumber = min + (long)(random.NextDouble() * (max - min));
            newAnnualDeposit.annualDepositNumber = randomDigitNumber.ToString();
            _context.annualDeposits.Add(newAnnualDeposit);
            await _context.SaveChangesAsync();
            return newAnnualDeposit;
        }

        public async Task<Account> TransferAnnualDepositToAccount(int customerId, int annualDepositId)
        {
            var customer = await _context.customers.Where(a => a.id == customerId).FirstOrDefaultAsync();
            if (customer == null)
            {
                throw new ArgumentException("customer is not found");
            }
            var account = await _context.accounts.Where(a => a.customerId == customerId).FirstOrDefaultAsync();
            if (account == null)
            {
                throw new ArgumentException("account is not found");
            }

            var annualDeposit = await getAnnualDeposit(annualDepositId);
            if (DateTime.Today<annualDeposit.AnnualDepositRepaymentTime)
            {
                throw new ArgumentException($"you can't deposit the annualDeposit until {annualDeposit.AnnualDepositRepaymentTime}");
            }

            account.balance += annualDeposit.AnnualDepositRepaymentValue;
            _context.annualDeposits.Remove(annualDeposit);
            await _context.SaveChangesAsync();
            return account;
        }

        private async Task<int> transferTheReturnToAccount(int annualDepositId)
        {
            var annualDeposit=await _context.annualDeposits.Where(a=>a.id == annualDepositId&& a.annualDepositType==AnnualDepositType.threeYears ).FirstOrDefaultAsync();
            if (annualDeposit == null || annualDeposit.acceptAnnualDeposit==false)
            {
                throw new ArgumentException("annual deposit is not found");
            }

            var account = await _context.accounts.Where(a => a.customerId == annualDeposit.customerId).FirstOrDefaultAsync();
            if(account == null)
            {
                throw new ArgumentException("account is not found");
            }
            var month = 12;
            var x = 0;
            while (DateTime.Today >= annualDeposit.AnnualDepositRequestTime.AddMonths(month))
            {
                account.balance += (annualDeposit.AnnualDepositRepaymentValue-annualDeposit.AnnualDepositValue)/(annualDeposit.AnnualDepositPeriod/12);
                annualDeposit.allowableCashingFromAnnualDeposit += (annualDeposit.AnnualDepositRepaymentValue - annualDeposit.AnnualDepositValue) / (annualDeposit.AnnualDepositPeriod / 12);
                await _context.SaveChangesAsync();
                Console.WriteLine(annualDeposit.allowableCashingFromAnnualDeposit);
                month+=12;
                if (month > annualDeposit.AnnualDepositPeriod)
                {
                    break;
                }
            }
            return annualDeposit.allowableCashingFromAnnualDeposit;
        }
    }
}
