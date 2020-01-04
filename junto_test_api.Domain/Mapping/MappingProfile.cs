using AutoMapper;
using junto_test_api.Entity;
using System;

namespace junto_test_api.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountViewModel, Account>();
            CreateMap<Account, AccountViewModel>();
            CreateMap<UserViewModel, User>();
            CreateMap<User, UserViewModel>();
            CreateMap<TokenViewModel, Token>();
            CreateMap<Token, TokenViewModel>();
        }

    }
}
