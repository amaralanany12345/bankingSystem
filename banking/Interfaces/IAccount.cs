using banking.Dto;
using banking.Migrations;
using banking.Models;

namespace banking.Interfaces
{
    public interface IAccount
    {
        Task<Account> createAccount(int customerId, int balance);
        Task<Account> getAccount(string accountNumber);
        //Task</>/Account updateAccount(Account account);
        Task deleteAccount(string accountNumber);
        Task<Deposit> deposit(string accountNumber, int value);
        Task<Withdraw> withdraw(string accountNumber, int value);
    }
}
