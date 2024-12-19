using banking.Models;

namespace banking.Interfaces
{
    public interface IUser
    {
        Task<User> createUser(User user);
        Task<User> getUser(int id);
        Task deleteUser(int id);
    }
}
