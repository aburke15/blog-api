
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Api.Controllers.Dtos;
using Blog.Data.Repositories.Interfaces;
using MediatR;

namespace Blog.Api.Infrastructure.Queries.Handlers
{
    public class GetAllPostSummariesQueryHandler : IRequestHandler<GetAllPostSummariesQuery, IEnumerable<PostSummaryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;

        public GetAllPostSummariesQueryHandler(
            IMapper mapper,
            IRepositoryWrapper repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<PostSummaryResponse>> Handle(
            GetAllPostSummariesQuery request,
            CancellationToken cancellationToken)
        {
            var posts = await _repository.Posts
                .GetAllAsync(cancellationToken);

            // replace with auto mapper
            return posts.Select(p => new PostSummaryResponse
            {
                Id = p.Id,
                CreatedAt = p.CreatedAt,
                CreatedAtDisplay = p.CreatedAt.ToString("hh:mm tt dddd yyyy-MM-dd"),
                Title = p.Title,
                // CanEdit = _userId == p.Author.Id,
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