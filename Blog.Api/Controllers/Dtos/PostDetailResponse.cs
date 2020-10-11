using System;

namespace Blog.Api.Controllers.Dtos
{
    public class PostDetailResponse : PostSummaryResponse
    {
        public string Body { get; set; }
    }
}