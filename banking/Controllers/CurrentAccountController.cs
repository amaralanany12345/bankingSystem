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
    public class CurrentAccountController : ControllerBase
    {
        private readonly CurrentAccountService _currentAccountService;
        private readonly IMapper _mapper;

        public CurrentAccountController(CurrentAccountService currentAccountService, IMapper mapper)
        {
            _currentAccountService = currentAccountService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<Account>> createCurrentAccount(int customerId, int balance)
        {
            return Ok(await _currentAccountService.createAccount(customerId,balance));
        }

        [HttpGet]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<AccountDto>> getAccount(string accountNumber)
        {
            return Ok(_mapper.Map<AccountDto>(await _currentAccountService.getAccount(accountNumber)));
        }

        [HttpPut("deposit")]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<Deposit>> deposit(string accountNumber, int value)
        {
            return Ok(await _currentAccountService.deposit(accountNumber,value));
        }

        [HttpPut("withdraw")]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<Deposit>> withdraw(string accountNumber, int value)
        {
            return Ok(await _currentAccountService.withdraw(accountNumber, value));
        }

        [HttpDelete]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<Deposit>> deleteCurrentAccount(string accountNumber)
        {
            await _currentAccountService.deleteAccount(accountNumber);
            return Ok();
        }
        [HttpPut]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<Transfer>> transferToAnotherAccount(int sentAccountId, int receivedAccountId, int transferValue)
        {
            return Ok(await _currentAccountService.TransferToAnotherAccount(sentAccountId, receivedAccountId, transferValue));
        }
    }
}
