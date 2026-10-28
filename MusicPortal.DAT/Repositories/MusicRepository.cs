using Microsoft.EntityFrameworkCore;
using MusicPortal.DAT.EF;
using MusicPortal.DAT.Interfaces;
using MusicPortal.DAT.Models;
using MusicPortal.DAT.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MusicPortal.DAT.Repositories
{
    public class MusicRepository(MusicPortalContext _db) : IRepository<Music>
    {
        public async Task<IEnumerable<Music>> GetListAsync()
        {
            var musics = _db.Musics.Include(m => m.Genre).ToList();
            return musics;
        }

        public Music FirstOrDefault(object value)
        {
            if (value is int Id)
            {
                var music = _db.Musics.FirstOrDefault(m => m.Id == Id);
                return music!;
            }
            return null!;
        }

        public void Add(object value)
        {
            if (value is Music music)
            {
                _db.Musics.Add(music);
            }
        }

        public void Update(object value)
        {
            if (value is Music music)
            {
                _db.Musics.Update(music);
            }
        }

        public void Remove(object value)
        {
            if (value is Music music)
            {
                _db.Musics.Remove(music);
            }
        }

        //public object IncludeToList()
        //{
        //    var musics = _db.Musics.Include(m => m.Genre).ToList();
        //    return musics;
        //}
    }
}
