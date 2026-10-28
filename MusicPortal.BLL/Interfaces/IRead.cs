using MusicPortal.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.BLL.Interfaces
{
    public interface IRead
    {
        IEnumerable<LanguageDTO> GetList();
    }
}
