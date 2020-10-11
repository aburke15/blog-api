
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories.Implementations
{
    public class SqlRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;

        public SqlRepository(DbContext context)
            => _context = context;

        public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
            => await _context.Set<T>()
                .AddAsync(entity, cancellationToken);

        public void Delete(T entity)
            => _context.Set<T>().Remove(entity);

        public async Task<IEnumerable<T>> GetAllAsync(
            CancellationToken cancellationToken)
            => await _context.Set<T>()
                .AsNoTracking<T>()
                .ToListAsync(cancellationToken);

        public async Task<T> GetOneAsync(int id, CancellationToken cancellationToken = default)
            => await _context.Set<T>().FindAsync(id, cancellationToken);

        public void Update(T entity)
            => _context.Set<T>().Update(entity);
    }
}