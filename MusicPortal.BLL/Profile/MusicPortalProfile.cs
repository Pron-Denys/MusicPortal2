using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.BLL.Profile
{
    using AutoMapper;
    using MusicPortal.DAT.Models;
    using MusicPortal.BLL.DTO;
    using MusicPortal.DAL.Entities;

    public class MusicPortalProfile : Profile
    {
        public MusicPortalProfile()
        {
            CreateMap<Genre, GenreDTO>();
            CreateMap<Music, MusicDTO>()
                .ForMember(m => m.Genre, temp => temp.MapFrom(m => m.Genre!.Name != null ? m.Genre!.Name : null))
                .ForMember(m => m.Songwriter, temp => temp.MapFrom(m => m.Songwriter!.Name != null ? m.Songwriter.Name : null));
            CreateMap<User, UserDTO>();
            CreateMap<Songwriter,  SongwriterDTO>();
        }
    }
}
