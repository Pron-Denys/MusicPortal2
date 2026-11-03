using MusicPortal.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.BLL.Interfaces
{
    public interface ISongwriterService
    {
        SongwriterDTO GetSongwriter(object value);
        Task<IEnumerable<SongwriterDTO>> GetListSongwritersAsync();
        void AddSongwriter(SongwriterDTO songwriterDTO);
        void UpdateSongwriter(SongwriterDTO songwriterDTO);
        void RemoveSongwriter(SongwriterDTO songwriterDTO);
    }
}
