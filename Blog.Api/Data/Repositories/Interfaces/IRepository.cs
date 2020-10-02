using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Data.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetOneAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
