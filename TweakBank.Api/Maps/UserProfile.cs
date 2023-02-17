using AutoMapper;
using TweakBank.Api.DTO;
using TweakBank.Models;

namespace TweakBank.Api.Maps
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            MapAccount();
            MapCustomer();
        }

        public void MapCustomer()
        {

            CreateMap<CreateCustomerDto, Customer>()
                    .ForMember(dest =>
                        dest.CustomerId,
                        opt => opt.Ignore());

            CreateMap<Customer, CreateCustomerDto>()
               .ForMember(dest =>
                   dest.BankAccount,
                   opt => opt.Ignore());

        }

        public void MapAccount()
        {
            CreateMap<CreateBankForExistingCustomerAccountDto, BankAccount>()
                .ForMember(dest =>
                    dest.BankAccountId,
                    opt => opt.Ignore())
                .ForMember(dest =>
                    dest.Balance,
                    opt => opt.MapFrom(src => src.InitialDeposit))
                .ForMember(dest =>
                    dest.AccountType,
                    opt => opt.MapFrom(src => src.AccountType))
                .ForMember(dest =>
                    dest.Customer,
                    opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
