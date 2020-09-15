using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Models
{
    public class Post
    {
        public Post()
            => CreatedAt = DateTime.Now;

        [Key]
        public int Id { get; }
        public DateTime CreatedAt { get; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string AuthorId { get; set; }
        public virtual User Author { get; set; }
    }
}