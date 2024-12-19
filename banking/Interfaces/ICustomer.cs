using banking.Dto;
using banking.Models;

namespace banking.Interfaces
{
    public interface ICustomer
    {
        Task<Customer> createCustomer(CustomerDto customer);
        Task<SigningResponse> signin(string email, string password);
        Task<SigningResponse> signup(Customer user);
        Task<Customer> getCustomer(int customerId);
        Task deleteCustomer(int id);
    }
}
