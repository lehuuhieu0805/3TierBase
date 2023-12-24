using _3TierBase.Business.Services;
using _3TierBase.Business.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace _3TierBase.Business
{
    public static class ModuleRegister
    {
        public static IServiceCollection RegisterBusiness(this IServiceCollection services)
        {
            services.RegisterServivce();
            services.ConfigureAutoMapper();
            return services;
        }
    }
}
