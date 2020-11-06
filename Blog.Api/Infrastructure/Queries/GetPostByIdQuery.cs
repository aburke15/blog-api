
using Blog.Api.Controllers.Dtos;
using MediatR;

namespace Blog.Api.Infrastructure.Queries
{
    public class GetPostByIdQuery : IRequest<PostDetailResponse>
    {
        public GetPostByIdQuery(int postId)
            => PostId = postId;

        public int PostId { get; private set; }
    }
}