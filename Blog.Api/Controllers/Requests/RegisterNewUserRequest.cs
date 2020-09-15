using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Api.Controllers.Requests
{
    public class RegisterNewUserRequest : UserLoginRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}