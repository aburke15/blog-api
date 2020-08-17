
using System.ComponentModel.DataAnnotations;

namespace Blog.Api.Controllers.Requests
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}