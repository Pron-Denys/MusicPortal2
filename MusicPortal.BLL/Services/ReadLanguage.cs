using Microsoft.Extensions.Configuration;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.BLL.Services
{
    public class ReadLanguage : IRead
    {
        public IEnumerable<LanguageDTO> Languages { get; set; }
        public ReadLanguage(IConfiguration configuration)
        {
            Languages = configuration.GetSection("Lang").GetChildren()
                .Select(l => new LanguageDTO { Code = l.Key, Name = l.Value });
        }

        public IEnumerable<LanguageDTO> GetList() => Languages;
    }
}
