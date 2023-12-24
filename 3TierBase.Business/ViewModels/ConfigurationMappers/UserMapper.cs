using _3TierBase.Business.ViewModels.Users;
using _3TierBase.Data.Entities;
using AutoMapper;

namespace _3TierBase.Business.ViewModels.ConfigurationMappers
{
    public static class UserMapper
    {
        public static void ConfigUser(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, GetUserDetailModel>().ReverseMap();
            configuration.CreateMap<User, CreateUserModel>().ReverseMap();
            configuration.CreateMap<User, UpdateUserModel>().ReverseMap();
        }
    }
}