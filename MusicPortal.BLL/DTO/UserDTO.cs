using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int Year { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public bool IsActivated { get; set; }
        public bool IsAdmin { get; set; }
    }
}
