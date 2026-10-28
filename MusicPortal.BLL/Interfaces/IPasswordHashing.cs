using MusicPortal.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.BLL.Interfaces
{
    public interface IPasswordHashing
    {
        PasswordAndSaltDTO Hashing(string password);
        bool PassowordEquals(string salt, string password, string userPassword);
    }
}
