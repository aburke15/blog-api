
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories.Implementations
{
    public class PostRepository : SqlRepository<Post>, IPostRepository
    {
        public PostRepository(BlogDbContext context)
            : base(context)
        { }

        public override async Task<IEnumerable<Post>> GetAllAsync(CancellationToken cancellationToken)
            => await BlogDbContext.Posts
                .Include(p => p.Author)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

        public BlogDbContext BlogDbContext
            => _context as BlogDbContext;
    }
}