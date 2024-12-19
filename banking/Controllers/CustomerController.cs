using AutoMapper;
using banking.Dto;
using banking.Models;
using banking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace banking.Controllers
{
    [Route("/api[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(CustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<CustomerDto>> createCustomer(CustomerDto customer)
        {
            return Ok(_mapper.Map<CustomerDto>(await _customerService.createCustomer(customer)));
        }

        [HttpGet]
        //[Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<CustomerDto>> getCustomer(int id)
        {
            return Ok(_mapper.Map<CustomerDto>(await _customerService.getCustomer(id)));
        }
        [HttpDelete]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult> deleteCustomer(int id)
        {
            await _customerService.deleteCustomer(id);
            return Ok();
        }

        [HttpPost("signIn")]
        public async Task<ActionResult<SigningResponse>> signin(string email, string password)
        {
            return Ok(await _customerService.signin(email,password));
        }
    }
}
