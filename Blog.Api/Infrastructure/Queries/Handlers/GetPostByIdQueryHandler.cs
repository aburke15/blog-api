
using System.Threading;
using System.Threading.Tasks;
using Blog.Api.Controllers.Dtos;
using Blog.Data.Repositories.Interfaces;
using MediatR;

namespace Blog.Api.Infrastructure.Queries.Handlers
{
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostDetailResponse>
    {
        private readonly IRepositoryWrapper _repository;

        public GetPostByIdQueryHandler(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public Task<PostDetailResponse> Handle(
            GetPostByIdQuery request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}