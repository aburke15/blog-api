
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Blog.Data.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<IdentityUser>
    {
        Task<IdentityUser> GetOneAsync(string id);
    }
}