
using Blog.Data.Models;
using Blog.Data.Repositories.Interfaces;

namespace Blog.Data.Repositories.Implementations
{
    public class PostRepository : SqlRepository<Post>, IPostRepository
    {
        public PostRepository(BlogDbContext context)
            : base(context)
        {
        }
    }
}