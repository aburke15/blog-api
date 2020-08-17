
using System;
using System.Threading.Tasks;
using Blog.Api.Controllers.Requests;
using Blog.Data;
using Blog.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("auth")]
    public class AuthenticationController : BaseApiController
    {
        private readonly BlogContext _context;

        public AuthenticationController(BlogContext context)
            => _context = context;
        // register
        [HttpGet("register/{id:int}")]
        public async Task<IActionResult> GetRegisteredUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound(new { Message = "User does not exist." });

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(CreateUserRequest request)
        {
            var user = new User
            {
                CreatedAt = DateTime.Now,
                Username = request.Username,
                Password = request.Password,
                Email = request.Email
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetRegisteredUser), new
                {
                    id = user.Id
                }, user);
        }

        [HttpDelete("register/{id:int}")]
        public async Task<IActionResult> DeleteRegisteredUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound(new { Message = "User does not exist." });

            _context.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"User with username: {user.Username} deleted." });
        }

        public (bool isGood, string name) DoesSomething()
        {
            return (true, "hello, world!");
        }
        // login
    }
}