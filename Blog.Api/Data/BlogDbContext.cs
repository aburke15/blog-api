using System;
using Blog.Data.Models;
using Blog.Data.Models.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public class BlogDbContext : IdentityDbContext<User>
    {
        public BlogDbContext(DbContextOptions options)
            : base(options)
        { }

        public virtual DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new PostConfiguration());
        }
    }
}
