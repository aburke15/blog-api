
using System.Threading.Tasks;

namespace Blog.Data.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        IUserRepository Users { get; }
        Task SaveChangesAsync();
    }
}