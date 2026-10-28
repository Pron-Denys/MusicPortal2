using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.BLL.Services
{
    using MusicPortal.DAT.Models;
    using MusicPortal.DAT.Interfaces;
    using MusicPortal.BLL.DTO;
    using AutoMapper;
    using MusicPortal.BLL.Interfaces;

    public class MusicService(IUnitOfWork UnitOfWork,  IMapper mapper) : IMusicService
    {
        public async Task<IEnumerable<MusicDTO>> GetIncludeToListMusics()
        {
            var musics = await UnitOfWork.Musics.GetListAsync();
            return mapper.Map<IEnumerable<MusicDTO>>(musics);
        }

        public MusicDTO GetMusicFirstOrDefault(object value)
        {
            var music = UnitOfWork.Musics.FirstOrDefault(value);
            return mapper.Map<MusicDTO>(music);
        }

        public void AddMusic(MusicDTO musicDTO)
        {
            var genre = UnitOfWork.Genres.FirstOrDefault(musicDTO.Genre!);
            Music music = new Music { Name = musicDTO.Name, Audio = musicDTO.Audio, Genre = genre, Poster = musicDTO.Poster, Songwriter = musicDTO.Songwriter, Year = musicDTO.Year };
            UnitOfWork.Musics.Add(music);
            UnitOfWork.Save();
        }

        public void UpdateMusic(MusicDTO musicDTO)
        {
            var genre = UnitOfWork.Genres.FirstOrDefault(musicDTO.Genre!);
            var music = UnitOfWork.Musics.FirstOrDefault(musicDTO.Id);
            music.Id = musicDTO.Id;
            music.Name = musicDTO.Name;
            music.Audio = musicDTO.Audio;
            music.Genre = genre;
            music.Poster = musicDTO.Poster;
            music.Songwriter = musicDTO.Songwriter;
            music.Year = musicDTO.Year;
            UnitOfWork.Musics.Update(music);
            UnitOfWork.Save();
        }

        public void RemoveMusic(MusicDTO musicDTO)
        {
            var genre = UnitOfWork.Genres.FirstOrDefault(musicDTO.Genre!);
            Music music = new Music { Name = musicDTO.Name, Audio = musicDTO.Audio, Genre = genre, Poster = musicDTO.Poster, Songwriter = musicDTO.Songwriter, Year = musicDTO.Year };
            UnitOfWork.Musics.Remove(music);
            UnitOfWork.Save();
        }
    }
}
