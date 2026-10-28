using MusicPortal.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.BLL.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDTO>> GetListGenresAsync();
        GenreDTO GetGenreFirhstOrDefault(object value);
        void AddGenre(GenreDTO genreDTO);
        void UpdateGenre(GenreDTO genreDTO);
        void RemoveGenre(GenreDTO genreDTO);
    }
}
