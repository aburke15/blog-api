using System.Threading;
using System.Threading.Tasks;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Blog.Data.Repositories.Implementations
{
    public class UserRepository : SqlRepository<IdentityUser>, IUserRepository
    {
        public UserRepository(BlogDbContext context)
            : base(context)
        { }

        public async Task<IdentityUser> GetOneAsync(string id, CancellationToken cancellationToken = default)
            => await BlogDbContext.Users.FindAsync(id, cancellationToken);

        public BlogDbContext BlogDbContext
        {
            get { return _context as BlogDbContext; }
        }
    }
}