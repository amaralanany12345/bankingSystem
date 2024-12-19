using banking.Models;

namespace banking.Interfaces
{
    public interface IFinance
    {
        Task<Finance> requestFinance(int customerId, int financeValue, int FinancePeriod);
        Task<Finance> getFinance(int financeId);
        Task<Finance> acceptFinance(int financeId);
        Task deleteFinance(int financeId);
        Task<Finance> repaidFinance(int financeId);

    }
}
