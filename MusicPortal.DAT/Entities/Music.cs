using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.DAT.Models
{
    public class Music
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Year { get; set; }
        public string? Songwriter { get; set; }
        public string? Audio { get; set; }
        public string? Poster { get; set; }
        public Genre? Genre { get; set; }
    }
}
