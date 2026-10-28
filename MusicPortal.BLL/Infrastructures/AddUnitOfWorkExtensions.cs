using Microsoft.Extensions.DependencyInjection;
using MusicPortal.DAT.Interfaces;
using MusicPortal.DAT.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.BLL.Infrastructures
{
    public static class AddUnitOfWorkExtensions
    {
        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
