using MusicPortal.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.BLL.Interfaces
{
    public interface IMusicService
    {
        Task<IEnumerable<MusicDTO>> GetIncludeToListMusics();
        MusicDTO GetMusicFirstOrDefault(object value);
        void AddMusic(MusicDTO musicDTO);
        void UpdateMusic(MusicDTO musicDTO);
        void RemoveMusic(MusicDTO musicDTO);
    }
}
