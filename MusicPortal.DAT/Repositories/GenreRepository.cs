using Microsoft.EntityFrameworkCore;
using MusicPortal.DAT.EF;
using MusicPortal.DAT.Interfaces;
using MusicPortal.DAT.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.DAT.Repositories
{
    public class GenreRepository(MusicPortalContext _db) : IRepository<Genre>
    {
        public async Task<IEnumerable<Genre>> GetListAsync()
        {
            var genres = await _db.Genres.AsNoTracking().ToListAsync();
            return genres;
        }

        public Genre FirstOrDefault(object value)
        {
            if (value is string Name)
            {
                Genre? genre = _db.Genres.FirstOrDefault(g => g.Name == Name);
                return genre!;
            }
            else if (value is int Id)
            {
                Genre? genre = _db.Genres.FirstOrDefault(g => g.Id == Id);
                return genre!;
            }
            return null!;
        }

        public void Add(object value)
        {
            if (value is Genre genre)
            {
                _db.Genres.Add(genre);
            }
        }

        public void Update(object value)
        {
            if (value is Genre genre)
            {
                _db.Genres.Update(genre);
            }
        }

        public void Remove(object value)
        {
            if (value is Genre genre)
            {
                _db.Remove(genre);
            }
        }
    }
}
