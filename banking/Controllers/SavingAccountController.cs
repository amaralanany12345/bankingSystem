using AutoMapper;
using banking.Dto;
using banking.Models;
using banking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavingAccountController : ControllerBase
    {
        private readonly SavingAccountService _savingAccountService;
        private readonly IMapper _mapper;

        public SavingAccountController(SavingAccountService savingAccountService, IMapper mapper)
        {
            _savingAccountService = savingAccountService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<Account>> createCurrentAccount(int customerId, int balance)
        {
            return Ok(await _savingAccountService.createAccount(customerId, balance));
        }

        [HttpGet]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<AccountDto>> getAccount(string accountNumber)
        {
            return Ok(_mapper.Map<AccountDto>(await _savingAccountService.getAccount(accountNumber)));
        }

        [HttpPut("deposit")]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<Deposit>> deposit(string accountNumber, int value)
        {
            return Ok(await _savingAccountService.deposit(accountNumber, value));
        }

        [HttpPut("withdraw")]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<Deposit>> withdraw(string accountNumber, int value)
        {
            return Ok(await _savingAccountService.withdraw(accountNumber, value));
        }

        [HttpDelete]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<Deposit>> deleteSavingAccount(string accountNumber)
        {
            await _savingAccountService.deleteAccount(accountNumber);
            return Ok();
        }
    }
}
