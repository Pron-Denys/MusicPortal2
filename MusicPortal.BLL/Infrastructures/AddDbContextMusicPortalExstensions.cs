using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.BLL.Infrastructures
{
    using Microsoft.EntityFrameworkCore;
    using MusicPortal.DAT.EF;
    public static class AddDbContextMusicPortalExstensions
    {
        public static void AddDbContextMusicPortal(this IServiceCollection services, string connection)
        {
            services.AddDbContext<MusicPortalContext>(db => db.UseSqlServer(connection));
        }
    }
}
