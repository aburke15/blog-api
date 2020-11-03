
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories.Implementations
{
    public class SqlRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;

        public SqlRepository(DbContext context)
            => _context = context;

        public virtual async Task CreateAsync(
            TEntity entity, CancellationToken cancellationToken = default)
                => await _context.Set<TEntity>()
                    .AddAsync(entity, cancellationToken);

        public virtual void Delete(TEntity entity)
            => _context.Set<TEntity>().Remove(entity);

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
            CancellationToken cancellationToken)
                => await _context.Set<TEntity>()
                    .ToListAsync(cancellationToken);

        public virtual async Task<TEntity> GetOneAsync(
            int id, CancellationToken cancellationToken = default)
                => await _context.Set<TEntity>().FindAsync(id, cancellationToken);

        public virtual void Update(TEntity entity)
            => _context.Set<TEntity>().Update(entity);
    }
}