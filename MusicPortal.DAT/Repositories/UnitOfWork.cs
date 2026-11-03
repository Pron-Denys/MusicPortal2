using MusicPortal.DAT.Interfaces;
using MusicPortal.DAT.EF;
using MusicPortal.DAT.Models;
using MusicPortal.DAT.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using MusicPortal.DAL.Repositories;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAT.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MusicPortalContext Database;
        private GenreRepository? GenreRepository;
        private MusicRepository? MusicRepository;
        private UserRepository? UserRepository;

        private SongwriterRepository? SongwriterRepository;

        public UnitOfWork(MusicPortalContext database)
        {
            Database = database;
            if (GenreRepository == null) GenreRepository = new GenreRepository(database);
            if (SongwriterRepository == null) SongwriterRepository = new SongwriterRepository(database);
            if (MusicRepository == null) MusicRepository = new MusicRepository(database);
            if (UserRepository == null) UserRepository = new UserRepository(database);
        }
        public IRepository<Genre> Genres 
        { 
            get { return GenreRepository!;  }               
        }

        public IRepository<Music> Musics
        {
            get { return MusicRepository!; }
        }

        public IRepository<User> Users
        {
            get { return UserRepository!; }
        }

        public IRepository<Songwriter> Songwriters
        {
            get { return SongwriterRepository!; }
        }

        public void Save()
        {
            Database.SaveChanges();
        }
    }
}
