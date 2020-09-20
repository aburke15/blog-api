using System;

namespace Blog.Api.Controllers.Dtos
{
    public class PostDetailDto : PostSummaryDto
    {
        public string Body { get; set; }
    }
}