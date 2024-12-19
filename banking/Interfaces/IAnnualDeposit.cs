using banking.Enum;
using banking.Models;

namespace banking.Interfaces
{
    public interface IAnnualDeposit
    {
        Task<AnnualDeposit> requestAnnualDeposit(int customerId, int AnnualDepositValue);
        Task<AnnualDeposit> getAnnualDeposit(int annualDepositId);
        Task<AnnualDeposit> acceptAnnualDeposit(int annualDepositId);
        Task<AnnualDepositCashing> deposit(int annualDepositId,int value);
        Task<Account> TransferAnnualDepositToAccount(int customerId,int annualDepositId);
        Task DeleteAnnualDeposit(int annualDepositId);
        Task<AnnualDeposit> reNewAnnualDeposit(int annualDepositId, int annualDepositPeriod, int value, AnnualDepositType annualDepositType);
        //protected Task<int> transferTheReturnToAccount(int annualDepositId);

    }
}