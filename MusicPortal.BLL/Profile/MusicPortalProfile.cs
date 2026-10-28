using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.BLL.Profile
{
    using AutoMapper;
    using MusicPortal.DAT.Models;
    using MusicPortal.BLL.DTO;
    public class MusicPortalProfile : Profile
    {
        public MusicPortalProfile()
        {
            CreateMap<Genre, GenreDTO>();
            CreateMap<Music, MusicDTO>()
                .ForMember(m => m.Genre, temp => temp.MapFrom(m => m.Genre!.Name != null ? m.Genre!.Name : null ));
            CreateMap<User, UserDTO>();
        }
    }
}
