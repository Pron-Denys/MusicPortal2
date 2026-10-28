using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.DAT.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MusicPortal.BLL.Services
{
    public class PasswordHashing : IPasswordHashing
    {
        public PasswordAndSaltDTO Hashing(string password)
        {
            var saltBytes = RandomNumberGenerator.GetBytes(16);
            var salt = Convert.ToHexString(SHA256.HashData(saltBytes));
            var passwordBytes = Encoding.Unicode.GetBytes(salt + password);
            var passwordHash = Convert.ToHexString(SHA256.HashData(passwordBytes));
            return new PasswordAndSaltDTO { Salt = salt, Password = passwordHash };
        }

        public bool PassowordEquals(string salt, string password, string userPassword)
        {
            var passwordBytes = Encoding.Unicode.GetBytes(salt + password);
            var tempPassword = Convert.ToHexString(SHA256.HashData(passwordBytes));
            return tempPassword == userPassword;
        }
    }
}
