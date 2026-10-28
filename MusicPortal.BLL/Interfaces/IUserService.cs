using MusicPortal.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUserListAsync();
        UserDTO GetUserFirstOrDefault(object value);
        void AddUser(UserDTO userDTO);
        void UpdateUser(UserDTO userDTO);
        void RemoveUser(UserDTO userDTO);
    }
}
