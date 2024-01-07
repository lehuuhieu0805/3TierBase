﻿using _3TierBase.Business.Services.MailServices;
using _3TierBase.Business.Services.File;
using _3TierBase.Business.Services.UserServices;
using _3TierBase.Business.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace _3TierBase.Business.Services
{   
    public static class ModuleRegister
    {
        public static void RegisterServivce(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtHelper, JwtHelper>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IFileService, FileService>();
        }
    }
}
