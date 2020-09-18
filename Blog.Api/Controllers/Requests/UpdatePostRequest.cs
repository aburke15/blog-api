using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Api.Controllers.Requests
{
    public class UpdatePostRequest : CreatePostRequest
    {
        [Required]
        public int Id { get; set; }
    }
}