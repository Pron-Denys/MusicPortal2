using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicPortal.BLL.DTO
{
    public class GenreDTO
    {
        public int Id { get; set; }
        [Display(ResourceType = typeof(Resources.Resource), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ErrorFieldIsRequired")]
        public string? Name { get; set; }
        [Display(ResourceType = typeof(Resources.Resource), Name = "ChoosePoster")]
        public string? Poster { get; set; }
    }
}
