using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Api.Controllers.Dtos;
using Blog.Api.Controllers.Requests;
using Blog.Data;
using Blog.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Blog.Api.Controllers
{
    [Route("posts")]
    public partial class PostController : BaseApiController
    {
        private readonly BlogDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<PostController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly string _username;
        private readonly string _userId;
        private readonly IMediator _mediator;

        public PostController(
            BlogDbContext context,
            IHttpContextAccessor httpContextAccessor,
            ILogger<PostController> logger,
            UserManager<User> userManager,
            IMediator mediator)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _userManager = userManager;
            _mediator = mediator;

            _username = _httpContextAccessor.HttpContext.User?.Identity?.Name;
            _userId = _context.Users.FirstOrDefault(u => u.UserName == _username)?.Id;
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetPostsAsync(CancellationToken cancellationToken)
        {
            var query = new object();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);

            var posts = await _context.Posts.Select(p =>
                new PostSummaryResponse
                {
                    Id = p.Id,
                    CreatedAt = p.CreatedAt,
                    CreatedAtDisplay = p.CreatedAt.ToString("hh:mm tt dddd yyyy-MM-dd"),
                    Title = p.Title,
                    CanEdit = _userId == p.Author.Id,
                    Author = new UserSummaryResponse
                    {
                        Username = p.Author.UserName
                    }
                })
                .OrderBy(p => p.Id)
                .ToListAsync(cancellationToken);

            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostByIdAsync(int id, CancellationToken cancellationToken)
        {
            var post = await _context.Posts
                .FindAsync(id, cancellationToken);

            if (post == null)
                return new NotFoundResult();

            var result = new PostDetailResponse
            {
                Id = post.Id,
                CreatedAt = post.CreatedAt,
                CreatedAtDisplay = post.CreatedAt.ToString("hh:mm tt dddd yyyy-MM-dd"),
                Title = post.Title,
                Body = post.Body,
                CanEdit = post.AuthorId == _userId,
                Author = new UserSummaryResponse
                {
                    Username = post.Author.UserName
                }
            };

            return Ok(result);
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> CreatePostAsync(
            [FromBody] CreatePostRequest request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager
                .FindByNameAsync(_username);

            // abstract this so that the data layer is not referenced in the presentation layer
            var post = new Post
            {
                Title = request.Title,
                Body = request.Body,
                AuthorId = user.Id,
                Author = user
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Post created with Id: [{post.Id}]" });
        }

        [HttpPut, Authorize]
        public async Task<IActionResult> UpdatePostAsync([FromBody] UpdatePostRequest request)
        {
            var post = await _context.Posts
                .FindAsync(request.Id);

            if (post == null)
                return new NotFoundResult();
            if (post.AuthorId != _userId)
                return new UnauthorizedResult();

            if (!string.IsNullOrEmpty(request.Title) && request.Title != post.Title)
                post.Title = request.Title;
            if (!string.IsNullOrEmpty(request.Body) && request.Body != post.Body)
                post.Body = request.Body;
            if (post.Author == null)
                post.Author = await _userManager.FindByIdAsync(_userId);

            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Post updated with Id: [{post.Id}]" });
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeletePostAsync(int id)
        {
            var post = await _context.Posts
                .FindAsync(id);

            if (post == null)
                return new NotFoundResult();
            if (post.AuthorId != _userId)
                return new UnauthorizedResult();

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Post deleted with Id: [{id}]" });
        }
    }
}