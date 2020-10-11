
using System.Threading;
using System.Threading.Tasks;
using Blog.Data.Repositories.Interfaces;

namespace Blog.Data.Repositories.Implementations
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly BlogDbContext _context;

        public RepositoryWrapper(BlogDbContext context)
        {
            _context = context;

            Posts = new PostRepository(_context);
            Users = new UserRepository(_context);
        }

        public IUserRepository Users { get; private set; }
        public IPostRepository Posts { get; private set; }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync(cancellationToken);
    }
}