using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Resources;
using System.Text;

namespace MusicPortal.DAT.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Poster { get; set; }
        public ICollection<Music>? Musics { get; set; }
    }
}
