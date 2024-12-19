using banking.Interfaces;
using banking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace banking.Services
{
    public class financeService : IFinance
    {
        private readonly AppDbContext _context;
        private int financeRepaymentPercentage => 30;
        public financeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Finance> acceptFinance(int financeId)
        {
            var finance=await getFinance(financeId);
            finance.acceptFinance = true;
            finance.FinanceRequestTime= DateTime.Now;
            finance.FinanceRepaymentTime = DateTime.Now.AddMonths(finance.FinancePeriod);
            await _context.SaveChangesAsync();
            await inActiveTheAccount(financeId);
            await _context.SaveChangesAsync();
            return finance;
        }

        public async Task<Finance> getFinance(int financeId)
        {
           var finance=await _context.finances.Where(a=>a.id==financeId).FirstOrDefaultAsync();
            if(finance==null)
            {
                throw new ArgumentException("finance is not found");
            }
            return finance;
        }

        public async Task<Finance> requestFinance(int customerId, int financeValue, int FinancePeriod)
        {
            var customer=await _context.customers.Where(a=>a.id == customerId).FirstOrDefaultAsync();
            if (customer == null) 
            {
                throw new ArgumentException("account is not found");
            }
            var newFinance = new Finance();
            newFinance.customerId = customerId;
            newFinance.customer=customer;
            newFinance.identityNumber = customer.identityNumber;
            newFinance.FinanceValue= financeValue;
            newFinance.FinanceRepaymentValue = newFinance.FinanceValue+ (int)(newFinance.FinanceValue * (float)(financeRepaymentPercentage) / (float)(100));
            newFinance.FinanceRequestTime = DateTime.Now;
            newFinance.FinancePeriod = FinancePeriod;
            newFinance.FinanceRepaymentTime = DateTime.Now.AddMonths(FinancePeriod);
            if (DateTime.Now > newFinance.FinanceRepaymentTime)
            {
                var account =await _context.accounts.Where(a=>a.customerId == customerId).FirstOrDefaultAsync();
                if(account==null)
                {
                    throw new ArgumentException("account is not found");
                }
                account.isActive = false;
            }

            var random = new Random();
            long min = 100000000000;
            long max = 999999999999;
            long randomDigitNumber = min + (long)(random.NextDouble() * (max - min));
            newFinance.FinanceNumber= randomDigitNumber.ToString();
            _context.finances.Add(newFinance);
            await _context.SaveChangesAsync();
            return newFinance;
        }

        public async Task deleteFinance(int financeId)
        {
            var finance=await getFinance(financeId);
            _context.finances.Remove(finance);
            await _context.SaveChangesAsync();
        }

        public async Task<Finance> repaidFinance(int financeId)
        {
            var finance = await getFinance(financeId);
            var account=await _context.accounts.Where(a=>a.customerId==finance.customerId).FirstOrDefaultAsync();
            if(account==null)
            {
                throw new ArgumentException("account is not found");
            }
            //_context.finances.Remove(finance);
            finance.FinanceRepaymentValue = 0;
            finance.isFinanceRepaid = true;
            await _context.SaveChangesAsync();
            return finance;
        }
        private async Task inActiveTheAccount(int financeId)
        {
            var finance=await getFinance(financeId);
            var account=await _context.accounts.Where(a=>a.customerId== finance.customerId).FirstOrDefaultAsync();
            if (account==null)
            {
                throw new ArgumentException("Account is not found");
            }

            if (DateTime.Today > finance.FinanceRepaymentTime)
            {
                account.isActive = false;
            }

        }
    }
}
