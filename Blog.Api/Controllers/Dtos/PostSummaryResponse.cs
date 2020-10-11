using System;

namespace Blog.Api.Controllers.Dtos
{
    public class PostSummaryResponse
    {
        public int? Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedAtDisplay { get; set; }
        public string Title { get; set; }
        public bool CanEdit { get; set; }

        public UserSummaryResponse Author { get; set; }
    }
}