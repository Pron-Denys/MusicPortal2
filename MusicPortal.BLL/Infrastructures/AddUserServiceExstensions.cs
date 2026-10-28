using Microsoft.Extensions.DependencyInjection;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.BLL.Infrastructures
{
    public static class AddUserServiceExstensions
    {
        public static void AddUserService(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
        }
    }
}
