using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.DAT.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetListAsync();
        T FirstOrDefault(object value);
        void Add(object value);
        void Update(object value);
        void Remove(object value);
    }
}
