using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Entities;
using MusicPortal.DAT.EF;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MusicPortal.DAT.Interfaces;

namespace MusicPortal.DAL.Repositories
{
    public class SongwriterRepository(MusicPortalContext _db) : IRepository<Songwriter>
    {
        public async Task<IEnumerable<Songwriter>> GetListAsync()
        {
            return await _db.Songwriters.ToListAsync();
        }

        public Songwriter FirstOrDefault(object value)
        {
            if (value is int id)
            {
                return _db.Songwriters.FirstOrDefault(s => s.Id == id)!;
            }
            if (value is string name)
            {
                return _db.Songwriters.FirstOrDefault(s => s.Name == name)!;
            }
            return null!;
        }

        public void Add(object value)
        {
            if (value is Songwriter songwriter)
            {
                _db.Songwriters.Add(songwriter);
            }
        }

        public void Update(object value)
        {
            if (value is Songwriter songwriter)
            {
                _db.Songwriters.Update(songwriter);
            }
        }

        public void Remove(object value)
        {
            if (value is Songwriter songwriter)
            {
                _db.Songwriters.Remove(songwriter);
            }
        }
    }
}
