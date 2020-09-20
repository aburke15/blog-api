using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Api.Controllers.Requests
{
    public class CreatePostRequest
    {
        [Required, MinLength(4), MaxLength(100)]
        public string Title { get; set; }
        public string Body { get; set; }
    }
}