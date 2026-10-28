using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MusicPortal.BLL.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.BLL.Infrastructures
{
    public static class AddMusicPortalProfileExstensions
    {
        public static void AddMusicPortalProfile(this IServiceCollection services)
        {
            services.AddAutoMapper(m => m.AddProfile<MusicPortalProfile>());           
        }
    }
}
