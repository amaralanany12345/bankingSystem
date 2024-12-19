using banking.Enum;

namespace banking.Models
{
    public class User
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public int age { get; set; }
        public string identityNumber { get; set; }
        public UserRole role { get; set; }
        public User setUserName(string userName) 
        {
            this.userName = userName;
            return this;
        }

        public User setEmail(string email)
        {
            this.email= email;
            return this;
        }
        public User setPassword(string password)
        {
            this.password = password;
            return this;
        }
        public User setPhone(string phone)
        {
            this.phone = phone;
            return this;
        }

        public User setAge(int age)
        {
            this.age= age;
            return this;
        }
        public User setIdentityNumber(string identityNumber)
        {
            this.identityNumber = identityNumber;
            return this;
        }
    }
}
