using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.BLL.Services
{
    using AutoMapper;
    using MusicPortal.DAT.Interfaces;
    using MusicPortal.BLL.DTO;
    using MusicPortal.DAT.Models;
    using MusicPortal.BLL.Interfaces;
    public class GenreService(IUnitOfWork UnitOfWork, IMapper mapper) : IGenreService
    {
        public async Task<IEnumerable<GenreDTO>> GetListGenresAsync()
        {
            var genres = await UnitOfWork.Genres.GetListAsync();
            return mapper.Map<IEnumerable<GenreDTO>>(genres);
        }

        public GenreDTO GetGenreFirhstOrDefault(object value)
        {
            var genre = UnitOfWork.Genres.FirstOrDefault(value);
            return mapper.Map<GenreDTO>(genre);
        }

        public void AddGenre(GenreDTO genreDTO)
        {
            Genre genre = new Genre { Name = genreDTO.Name, Poster = genreDTO.Poster };
            UnitOfWork.Genres.Add(genre);
            UnitOfWork.Save();
        }

        public void UpdateGenre(GenreDTO genreDTO)
        {
            var genre = UnitOfWork.Genres.FirstOrDefault(genreDTO.Id);
            genre.Name = genreDTO.Name;
            genre.Poster = genreDTO.Poster;
            UnitOfWork.Genres.Update(genre);
            UnitOfWork.Save();
        }

        public void RemoveGenre(GenreDTO genreDTO)
        {
            var genre = UnitOfWork.Genres.FirstOrDefault(genreDTO.Id);
            UnitOfWork.Genres.Remove(genre);
            UnitOfWork.Save();
        }
    }
}
