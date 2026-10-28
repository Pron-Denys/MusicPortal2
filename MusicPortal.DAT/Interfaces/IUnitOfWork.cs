using System;
using System.Collections.Generic;
using System.Text;
using MusicPortal.DAT.Models;

namespace MusicPortal.DAT.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Genre> Genres { get; }
        IRepository<Music> Musics { get; }
        IRepository<User> Users { get; }
        void Save();
    }
}
