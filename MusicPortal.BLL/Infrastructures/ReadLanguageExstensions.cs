using Microsoft.Extensions.DependencyInjection;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.BLL.Infrastructures
{
    public static class ReadLanguageExstensions
    {
        public static void AddReadLanguage(this IServiceCollection services)
        {
            services.AddScoped<IRead, ReadLanguage>();
        }
    }
}
