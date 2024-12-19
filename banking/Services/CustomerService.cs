using banking.Dto;
using banking.Interfaces;
using banking.Models;
using Microsoft.EntityFrameworkCore;

namespace banking.Services
{
    public class CustomerService : SigningService, ICustomer
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public CustomerService(AppDbContext context, Jwt jwt, IHttpContextAccessor contextAccessor) : base(jwt, contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public async Task<Customer> createCustomer(CustomerDto customer)
        {
            var newCustomer = new Customer();
            newCustomer.userName = customer.userName;
            newCustomer.email = customer.email;
            newCustomer.password = hashPassword(customer.password);
            newCustomer.phone = customer.phone;
            newCustomer.age = customer.age;
            newCustomer.identityNumber = customer.identityNumber;
            newCustomer.role = Enum.UserRole.user;
            _context.customers.Add(newCustomer);
            await _context.SaveChangesAsync();
            return newCustomer;
        }
        public async Task deleteCustomer(int id)
        {
           var customer=await getCustomer(id);
           _context.customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> getCustomer(int customerId)
        {
            if(customerId != getCurrentUserId())
            {
                throw new ArgumentException("you are not allowed to access this route");
            }
            var customer= await _context.customers.Where(a=>a.id==customerId).FirstOrDefaultAsync();
            if(customer==null)
            {
                throw new ArgumentException("customer is not found");
            }
            return customer;
        }

        public async Task<SigningResponse> signin(string email, string password)
        {
            var customer = await _context.customers.Where(a => a.email == email).FirstOrDefaultAsync();
            if (customer == null || !verifyPassword(password, customer.password))
            {
                throw new ArgumentException("customer is not found");
            }
            return new SigningResponse
            {
                user=customer,
                token=generateToken(customer),
            };
        }

        public async Task<SigningResponse> signup(Customer user)
        {
            var customer = new Customer();
            customer.userName = user.userName;
            customer.email = user.email;
            customer.password = hashPassword(user.password);
            customer.phone = user.phone;
            customer.age = user.age;
            customer.identityNumber = user.identityNumber;
            _context.customers.Add(customer);
            await _context.SaveChangesAsync();
            return new SigningResponse
            {
                user=customer,
                token=generateToken(customer),
            };

        }


    }
}
