using AutoMapper;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.Entities;
using MusicPortal.DAT.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.BLL.Services
{
    public class SongwriterService(IUnitOfWork unitOfWork, IMapper mapper) : ISongwriterService
    {
        public SongwriterDTO GetSongwriter(object value)
        {
            var songwriter = unitOfWork.Songwriters.FirstOrDefault(value);
            return mapper.Map<SongwriterDTO>(songwriter);
        }

        public async Task<IEnumerable<SongwriterDTO>> GetListSongwritersAsync()
        {
            var songwriters = await unitOfWork.Songwriters.GetListAsync();
            return mapper.Map<IEnumerable<SongwriterDTO>>(songwriters);
        }

        public void AddSongwriter(SongwriterDTO songwriterDTO)
        {
            Songwriter songwriter = new Songwriter { Name = songwriterDTO.Name, Photo = songwriterDTO.Photo };
            unitOfWork.Songwriters.Add(songwriter);
            unitOfWork.Save();
        }

        public void UpdateSongwriter(SongwriterDTO songwriterDTO)
        {
            Songwriter songwriter = unitOfWork.Songwriters.FirstOrDefault(songwriterDTO.Id);
            songwriter.Name = songwriterDTO.Name;
            songwriter.Photo = songwriterDTO.Photo;
            unitOfWork.Songwriters.Update(songwriter);
            unitOfWork.Save();
        }

        public void RemoveSongwriter(SongwriterDTO songwriterDTO)
        {
            Songwriter songwriter = unitOfWork.Songwriters.FirstOrDefault(songwriterDTO.Id);
            unitOfWork.Songwriters.Remove(songwriter);
            unitOfWork.Save();
        }       
    }
}
