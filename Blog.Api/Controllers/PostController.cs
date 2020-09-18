using System;
using System.Linq;
using System.Threading.Tasks;
using Blog.Api.Controllers.Dtos;
using Blog.Api.Controllers.Requests;
using Blog.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Blog.Api.Controllers
{
    [Route("posts")]
    public partial class PostController : BaseApiController
    {
        private readonly BlogDbContext _context;
        private readonly ILogger<PostController> _logger;

        public PostController(
            BlogDbContext context,
            ILogger<PostController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetPostsAsync()
        {
            var posts = await _context.Posts.Select(p =>
                new PostSummaryDto
                {
                    Id = p.Id,
                    CreatedAt = p.CreatedAt,
                    Title = p.Title,
                    Author = new UserSummaryDto
                    {
                        Id = p.AuthorId,
                        Username = p.Author.UserName
                    }
                }).ToListAsync();

            if (posts == null)
                return new BadRequestObjectResult(new { Message = "Something went wrong." });

            return Ok(posts);
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostRequest request)
        {
            // validation
            var post = new Data.Models.Post
            {
                Title = request.Title,
                Body = request.Body,
                AuthorId = request.AuthorId
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Post created successfully with id: [{post.Id}]" });
        }

        [HttpPut, Authorize]
        public async Task<IActionResult> UpdatePostAsync([FromBody] UpdatePostRequest request)
        {
            var post = await _context.Posts
                .FindAsync(request.Id);

            if (post == null || post.AuthorId != request.AuthorId)
                return new UnauthorizedObjectResult(new { Message = "Not authorized to update this post." });

            if (!string.IsNullOrEmpty(request.Title) && request.Title != post.Title)
                post.Title = request.Title;
            if (!string.IsNullOrEmpty(request.Body) && request.Body != post.Body)
                post.Body = request.Body;

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Post updated successfully." });
        }
    }
}