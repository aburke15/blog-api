using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories.Implementations
{
    public class UserRepository : SqlRepository<IdentityUser>, IUserRepository
    {
        public UserRepository(BlogDbContext context)
            : base(context)
        { }

        public async Task<IdentityUser> GetOneAsync(
            string id, CancellationToken cancellationToken = default)
                => await BlogDbContext.Users.FindAsync(id, cancellationToken);

        public async Task<IdentityUser> GetByUsernameAsync(
            string username, CancellationToken cancellationToken = default)
                => await BlogDbContext.Users.FirstOrDefaultAsync(u => u.UserName.Equals(username));

        private BlogDbContext BlogDbContext
            => _context as BlogDbContext;
    }
}