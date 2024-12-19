using banking.Enum;
using banking.Models;

namespace banking.Interfaces
{
    public interface IEmployee
    {
        Task<Employee> CreateEmployee(string userName, string email, string password, string phone, int age, string identityNumber);
        Task<Employee> getEmployee(int employeeId);
        Task deleteEmployee(int employeeId);
        Task<SigningResponse> signin(string email,string password);
    }
}
