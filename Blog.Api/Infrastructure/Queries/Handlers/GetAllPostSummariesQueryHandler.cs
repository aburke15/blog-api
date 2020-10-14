
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Api.Controllers.Dtos;
using Blog.Data.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Blog.Api.Infrastructure.Queries.Handlers
{
    public class GetAllPostSummariesQueryHandler : IRequestHandler<GetAllPostSummariesQuery, IEnumerable<PostSummaryResponse>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;

        public GetAllPostSummariesQueryHandler(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IRepositoryWrapper repository)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<PostSummaryResponse>> Handle(
            GetAllPostSummariesQuery request,
            CancellationToken cancellationToken)
        {
            var username = _httpContextAccessor.HttpContext.User?.Identity?.Name;
            var user = await _repository.Users.GetByUsernameAsync(username);

            var posts = await _repository.Posts
                .GetAllAsync(cancellationToken);

            // replace with auto mapper
            return posts.Select(p => new PostSummaryResponse
            {
                Id = p.Id,
                CreatedAt = p.CreatedAt,
                CreatedAtDisplay = p.CreatedAt.ToString("hh:mm tt dddd yyyy-MM-dd"),
                Title = p.Title,
                CanEdit = user?.Id == p.Author.Id,
                Author = new UserSummaryResponse
                {
                    Username = p.Author.UserName
                }
            })
            .OrderBy(p => p.Id)
            .ToList();
        }
    }
}