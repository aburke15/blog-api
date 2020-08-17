using System;
using System.Collections.Generic;

namespace Blog.Data.Models
{
    public partial class User
    {
        public User()
        {
            Post = new HashSet<Post>();
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Post> Post { get; set; }
    }
}
