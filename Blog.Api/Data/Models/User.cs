using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Blog.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Posts = new HashSet<Post>();
        }

        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}