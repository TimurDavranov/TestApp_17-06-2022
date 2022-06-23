using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestApp_17_06_2022.Data.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Get(long id);
        Task Create(T item);
        Task Update(T item);
        void Delete(long id);
    }
}