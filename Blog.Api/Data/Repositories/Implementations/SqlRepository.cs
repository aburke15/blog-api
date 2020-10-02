
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories.Implementations
{
    public class SqlRepository<T> : IRepository<T> where T : class
    {
        protected readonly BlogDbContext _context;

        public SqlRepository(BlogDbContext context)
            => _context = context;

        public void Create(T entity)
            => _context.Set<T>().Add(entity);

        public void Delete(T entity)
            => _context.Set<T>().Remove(entity);

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _context.Set<T>()
                .AsNoTracking<T>()
                .ToListAsync();

        public async Task<T> GetOneAsync(int id)
            => await _context.Set<T>().FindAsync(id);

        public void Update(T entity)
            => _context.Set<T>().Update(entity);
    }
}