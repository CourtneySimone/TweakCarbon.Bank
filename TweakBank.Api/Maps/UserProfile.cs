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
        }

        public void MapAccount()
        {
            CreateMap<CreateAccountDto, Account>()
                .ForMember(dest =>
                    dest.AccountId,
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
