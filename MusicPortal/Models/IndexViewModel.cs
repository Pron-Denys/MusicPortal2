using MusicPortal.BLL.DTO;

namespace MusicPortal.Models
{
    public record IndexViewModel
    {
        public IEnumerable<MusicDTO> Musics { get; set; } = [];
        public DataFilteringViewModel? Filter { get; set; }
        public PaginationViewModel? Page { get; set; }
        public SortViewModel? Sort { get; set; }
    }
}
