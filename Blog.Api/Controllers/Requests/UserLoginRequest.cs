using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Api.Controllers.Requests
{
    public class UserLoginRequest
    {
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
        [Required]
        public string Username { get; set; }
    }
}