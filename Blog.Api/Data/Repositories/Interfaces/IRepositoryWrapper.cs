
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Data.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        IUserRepository Users { get; }
        IPostRepository Posts { get; }
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}