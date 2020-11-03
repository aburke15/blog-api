
using Blog.Api.Controllers.Dtos;
using MediatR;

namespace Blog.Api.Infrastructure.Queries
{
    public class GetPostByIdQuery : IRequest<PostDetailResponse>
    { }
}