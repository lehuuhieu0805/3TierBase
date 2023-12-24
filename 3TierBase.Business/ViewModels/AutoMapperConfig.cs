using _3TierBase.Business.ViewModels.ConfigurationMappers;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace _3TierBase.Business.ViewModels;

public static class AutoMapperConfig
{
    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.ConfigUser();
        });
        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}