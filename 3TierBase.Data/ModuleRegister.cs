using _3TierBase.Data.Entities;
using _3TierBase.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace _3TierBase.Data
{
    public static class ModuleRegister
    {
        public static IServiceCollection RegisterData(this IServiceCollection services)
        {
            // Register DbContext
            services.AddScoped<DbContext, BlogContext>();
            services.RegisterRepository();
            return services;
        }
    }
}
