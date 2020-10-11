
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Api.Controllers.Dtos;
using Blog.Data.Repositories.Interfaces;
using MediatR;

namespace Blog.Api.Infrastructure.Queries.Handlers
{
    public class GetAllPostSummariesQueryHandler : IRequestHandler<GetAllPostSummariesQuery, IEnumerable<PostSummaryResponse>>
    {
        private readonly IPostRepository _postRepository;

        public GetAllPostSummariesQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<PostSummaryResponse>> Handle(
            GetAllPostSummariesQuery request,
            CancellationToken cancellationToken)
        {
            var posts = await _postRepository
                .GetAllAsync(cancellationToken);

            // auto mapper
            return posts.Select(x => new PostSummaryResponse
            {

            }).ToList();
        }
    }
}