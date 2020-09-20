using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Api.Controllers.Requests
{
    public class DeletePostRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string AuthorId { get; set; }
    }
}