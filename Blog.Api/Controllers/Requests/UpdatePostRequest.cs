using System;

namespace Blog.Api.Controllers.Requests
{
    public class UpdatePostRequest : CreatePostRequest
    {
        public int Id { get; set; }
    }
}