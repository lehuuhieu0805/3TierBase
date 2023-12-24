using _3TierBase.Data.Repositories;
using _3TierBase.Data.Repositories.UserRepository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3TierBase.Data.Services
{
    public static class ModuleRegister
    {
        public static void RegisterRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
