using banking.Enum;
using banking.Models;
using banking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        [Authorize(Roles ="manager,superAdmin")]
        public async Task<ActionResult<Employee>> createEmployee(string userName, string email, string password, string phone, int age, string identityNumber)
        {
            return Ok(await _employeeService.CreateEmployee(userName, email, password, phone, age, identityNumber));
        }


        [HttpPost("signin")]
        public async Task<ActionResult<Employee>> signin(string email, string password)
        {
            return Ok(await _employeeService.signin(email, password));
        }
        [HttpGet]
        [Authorize(Roles ="manager,superAdmin")]
        public async Task<ActionResult<Employee>> getEmployee(int employeeId)
        {
            return Ok(await _employeeService.getEmployee(employeeId));
        }
    }
}
