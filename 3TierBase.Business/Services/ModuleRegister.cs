using _3TierBase.Business.Services.UserServices;
using Microsoft.Extensions.DependencyInjection;

namespace _3TierBase.Business.Services
{   
    public static class ModuleRegister
    {
        public static void RegisterServivce(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
