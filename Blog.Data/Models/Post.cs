using System;
using System.Collections.Generic;

namespace Blog.Data.Models
{
    public partial class Post
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int? AuthorId { get; set; }

        public virtual User Author { get; set; }
    }
}
