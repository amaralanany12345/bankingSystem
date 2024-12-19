using AutoMapper;
using banking.Dto;
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
    public class AnnualDepositWithOneYearController : ControllerBase
    {
        private readonly AnnualDepositWithOneYearService _annualDepositWithOneYear;
        private readonly IMapper _mapper;

        public AnnualDepositWithOneYearController(AnnualDepositWithOneYearService annualDepositWithOneYear, IMapper mapper)
        {
            _annualDepositWithOneYear = annualDepositWithOneYear;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles ="employee")]
        public async Task<ActionResult<AnnualDeposit>> requestAnnualDeposit(int customerId, int AnnualDepositValue)
        {
            return Ok(_mapper.Map<AnnualDepositDto>(await _annualDepositWithOneYear.requestAnnualDeposit(customerId, AnnualDepositValue)));
        }

        [HttpGet]
        public async Task<ActionResult<AnnualDeposit>> getAnnualDeposit(int annualDepositId)
        {
            return Ok(_mapper.Map<AnnualDepositDto>(await _annualDepositWithOneYear.getAnnualDeposit(annualDepositId)));
        }

        [HttpPut("acceptAnnualDeposit")]
        [Authorize(Roles = "manager,superAdmin")]
        public async Task<ActionResult<AnnualDeposit>> acceptAnnualDeposit(int annualDepositId)
        {
            return Ok(_mapper.Map<AnnualDepositDto>(await _annualDepositWithOneYear.acceptAnnualDeposit(annualDepositId)));
        }

        [HttpPut("TransferAnnualDepositToAccount")]
        public async Task<ActionResult<AnnualDeposit>> TransferAnnualDepositToAccount(int customerId, int annualDepositId)
        {
            return Ok(_mapper.Map<AnnualDepositDto>(await _annualDepositWithOneYear.TransferAnnualDepositToAccount(customerId, annualDepositId)));
        }

        [HttpDelete]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult> deleteAnnualDeposit(int annualDepositId)
        {
            await _annualDepositWithOneYear.DeleteAnnualDeposit(annualDepositId);
            return Ok();
        }


        [HttpPut("deposit")]
        [Authorize(Roles ="employee,manager")]
        public async Task<ActionResult<AnnualDepositCashing>> depositAnnualDeposit(int annualDepositId, int value)
        {
            return Ok(await _annualDepositWithOneYear.deposit(annualDepositId, value));
        }

        [HttpPut("reNewAnnualDeposit")]
        [Authorize(Roles = "employee,manager,superAdmin")]
        public async Task<ActionResult<AnnualDeposit>> reNewAnnualDeposit(int annualDepositId, int annualDepositPeriod, int value, AnnualDepositType annualDepositType)
        {
            return Ok(await _annualDepositWithOneYear.reNewAnnualDeposit(annualDepositId, annualDepositPeriod, value, annualDepositType));
        }
    }
}
