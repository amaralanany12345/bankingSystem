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
    public class VipAccountController : ControllerBase
    {
        private readonly VipAccountService _vipAccountService;
        private readonly IMapper _mapper;

        public VipAccountController(VipAccountService vipAccountService, IMapper mapper)
        {
            _vipAccountService = vipAccountService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<Account>> createCurrentAccount(int customerId, int balance)
        {
            return Ok(await _vipAccountService.createAccount(customerId, balance));
        }

        [HttpGet]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<AccountDto>> getAccount(string accountNumber)
        {
            return Ok(_mapper.Map<AccountDto>(await _vipAccountService.getAccount(accountNumber)));
        }

        [HttpPut("deposit")]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<Deposit>> deposit(string accountNumber, int value)
        {
            return Ok(await _vipAccountService.deposit(accountNumber, value));
        }

        [HttpPut("withdraw")]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<Deposit>> withdraw(string accountNumber, int value)
        {
            return Ok(await _vipAccountService.withdraw(accountNumber, value));
        }

        [HttpDelete]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult> deleteVipAccount(string accountNumber)
        {
            await _vipAccountService.deleteAccount(accountNumber);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<Transfer>> transferToAnotherAccount(int sentAccountId,int receivedAccountId,int transferValue)
        {
            return Ok(await _vipAccountService.TransferToAnotherAccount(sentAccountId,receivedAccountId,transferValue));
        }
    }
}
