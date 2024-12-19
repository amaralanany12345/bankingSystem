using banking.Enum;
using banking.Interfaces;
using banking.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace banking.Services
{
    public class EmployeeService :SigningService, IEmployee
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public EmployeeService(AppDbContext context, Jwt jwt, IHttpContextAccessor contextAccessor) : base(jwt,contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public async Task<Employee> CreateEmployee(string userName, string email, string password, string phone, int age, string identityNumber)
        {
            var employee=new Employee();
            employee.userName = userName;
            employee.email = email;
            employee.password = hashPassword(password);
            employee.phone = phone;
            employee.age = age;
            employee.identityNumber = identityNumber;
            employee.role = UserRole.employee;
            _context.employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task deleteEmployee(int employeeId)
        {
            var employee=await getEmployee(employeeId);
            _context.employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee> getEmployee(int employeeId)
        {
            var currentEmployee = await _context.employees.Where(a => a.id == getCurrentUserId()).FirstOrDefaultAsync();
            if (currentEmployee == null)
            {
                throw new ArgumentException("current employee is not found");
            }
            if(currentEmployee.role!= UserRole.employee)
            {
                throw new ArgumentException("you are not allowed to access this route");
            }

            var employee=await _context.employees.Where(a=>a.id==employeeId).FirstOrDefaultAsync();
            if (employee == null)
            {
                throw new ArgumentException("employee is not found");
            }
            Console.WriteLine(employee.role);
            return employee;
        }

        public async Task<SigningResponse> signin(string email, string password)
        {
            var employee =await _context.employees.Where(a=>a.email==email).FirstOrDefaultAsync();
            if(employee == null || !verifyPassword(password, employee.password))
            {
                throw new ArgumentException("employee is not found");
            }

            return new SigningResponse
            {
                user=employee,
                token = generateToken(employee)
            };
        }

    }
}
