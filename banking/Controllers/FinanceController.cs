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
    public class FinanceController : ControllerBase
    {
        private readonly financeService _financeService;
        private readonly IMapper _mapper;

        public FinanceController(financeService financeService, IMapper mapper)
        {
            _financeService = financeService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<Finance>> requestFinance(int customerId, int financeValue,  int FinancePeriod)
        {
            return Ok(_mapper.Map<FinanceDto>(await _financeService.requestFinance(customerId,financeValue,FinancePeriod)));
        }

        [HttpGet]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<Finance>> getFinance(int financeId)
        {
            return Ok(_mapper.Map<FinanceDto>(await _financeService.getFinance(financeId)));
        }

        [HttpPut]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<Finance>> acceptFinance(int financeId)
        {
            return Ok(_mapper.Map<FinanceDto>(await _financeService.acceptFinance(financeId)));
        }

        [HttpPut("repaidFinance")]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<Finance>> repaidFinance(int financeId)
        {
            return Ok(_mapper.Map<FinanceDto>(await _financeService.repaidFinance(financeId)));
        }

        [HttpDelete]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult> deleteFinance(int financeId)
        {
            await _financeService.deleteFinance(financeId);
            return Ok();
        }
    }
}
