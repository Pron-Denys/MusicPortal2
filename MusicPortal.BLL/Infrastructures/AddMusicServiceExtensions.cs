using Microsoft.Extensions.DependencyInjection;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.BLL.Infrastructures
{
    public static class AddMusicServiceExtensions
    {
        public static void AddMusicService(this IServiceCollection services)
        {
            services.AddTransient<IMusicService, MusicService>();
        }
    }
}
