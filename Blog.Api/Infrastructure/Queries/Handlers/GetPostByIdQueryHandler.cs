
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Api.Controllers.Dtos;
using Blog.Data.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Blog.Api.Infrastructure.Queries.Handlers
{
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostDetailResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;

        public GetPostByIdQueryHandler(
            IHttpContextAccessor httpContextAccessor,
            IRepositoryWrapper repository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = repository;
        }

        public async Task<PostDetailResponse> Handle(
            GetPostByIdQuery request,
            CancellationToken cancellationToken)
        {
            var username = _httpContextAccessor.HttpContext.User?.Identity?.Name;
            var user = await _repository.Users.GetByUsernameAsync(username);

            var post = await _repository.Posts
                .GetOneAsync(request.PostId, cancellationToken);

            if (post == null) return null;

            return new PostDetailResponse
            {
                Id = post.Id,
                CreatedAt = post.CreatedAt,
                CreatedAtDisplay = post.CreatedAt.ToString("hh:mm tt dddd yyyy-MM-dd"),
                Title = post.Title,
                Body = post.Body,
                CanEdit = post.AuthorId == user?.Id,
                Author = new UserSummaryResponse
                {
                    Username = post.Author.UserName
                }
            };
        }
    }
}