using AutoMapper;
using MusicPortal.BLL.DTO;
using MusicPortal.DAT.Interfaces;
using MusicPortal.DAT.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.BLL.Services
{
    using MusicPortal.BLL.Interfaces;
    public class UserService(IUnitOfWork UnitOfWork, IMapper mapper) : IUserService
    {
        public async Task<IEnumerable<UserDTO>> GetUserListAsync()
        {
            var users = await UnitOfWork.Users.GetListAsync();
            return mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public UserDTO GetUserFirstOrDefault(object value)
        {
            var user = UnitOfWork.Users.FirstOrDefault(value);
            return mapper.Map<UserDTO>(user);
        }

        public void AddUser(UserDTO userDTO)
        {
            User user = new User { Name = userDTO.Name, Surname = userDTO.Surname, IsActivated = userDTO.IsActivated, IsAdmin = userDTO.IsAdmin, Login = userDTO.Login, Password = userDTO.Password, Salt = userDTO.Salt, Year = userDTO.Year };
            UnitOfWork.Users.Add(user);
            UnitOfWork.Save();
        }

        public void UpdateUser(UserDTO userDTO)
        {
            var user = UnitOfWork.Users.FirstOrDefault(userDTO.Id);
            user.Name = userDTO.Name;
            user.Surname = userDTO.Surname;
            user.Year = userDTO.Year;
            user.IsAdmin = userDTO.IsAdmin;
            user.IsActivated = userDTO.IsActivated;
            user.Password = userDTO.Password;
            user.Salt = userDTO.Salt;
            UnitOfWork.Users.Update(user);
            UnitOfWork.Save();
        }

        public void RemoveUser(UserDTO userDTO)
        {
            var user = UnitOfWork.Users.FirstOrDefault(userDTO.Id);
            UnitOfWork.Users.Remove(user);
            UnitOfWork.Save();
        }
    }
}
