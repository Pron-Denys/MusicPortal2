using MusicPortal.DAT.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.DAL.Entities
{
    public class Songwriter
    {
        public int Id {  get; set; }
        public string? Name { get; set; }
        public string? Photo {  get; set; }
        public ICollection<Music> Musics { get; set; } = [];
    }
}
