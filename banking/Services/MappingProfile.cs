using AutoMapper;
using banking.Dto;
using banking.Models;

namespace banking.Services
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            CreateMap<Account, AccountDto>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<Finance,FinanceDto>();
            CreateMap<AnnualDeposit,AnnualDepositDto>();
        }
    }
}
