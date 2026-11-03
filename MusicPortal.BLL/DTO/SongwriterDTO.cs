using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicPortal.BLL.DTO
{
    public class SongwriterDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "EnterTheName", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string? Name { get; set; }
        [Required(ErrorMessageResourceName = "ChoosePoster", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string? Photo { get; set; }
    }
}
