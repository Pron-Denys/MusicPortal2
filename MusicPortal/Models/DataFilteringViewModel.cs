using Microsoft.AspNetCore.Mvc.Rendering;
using MusicPortal.BLL.DTO;
namespace MusicPortal.Models
{
    public class DataFilteringViewModel
    {
        public string? Name { get; set; }
        public List<GenreDTO> Genres { get; set; }
        public GenreDTO SelectedGenre { get; set; }

        public DataFilteringViewModel(string name, IEnumerable<GenreDTO> genres, GenreDTO selectedGenre)
        {
            Name = name;
            var temp = new GenreDTO { Name = "Всі" };
            Genres = genres as List<GenreDTO>;
            Genres.Insert(0, temp);
            if (selectedGenre == null)
                SelectedGenre = temp;
            else
                SelectedGenre = selectedGenre;              
        }

    }
}
