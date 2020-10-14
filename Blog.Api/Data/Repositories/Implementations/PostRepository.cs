
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
    }
}