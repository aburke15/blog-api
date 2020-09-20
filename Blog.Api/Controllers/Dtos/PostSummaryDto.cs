using System;

namespace Blog.Api.Controllers.Dtos
{
    public class PostSummaryDto
    {
        public int? Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedAtDisplay { get; set; }
        public string Title { get; set; }
        public bool CanEdit { get; set; }

        public UserSummaryDto Author { get; set; }
    }
}