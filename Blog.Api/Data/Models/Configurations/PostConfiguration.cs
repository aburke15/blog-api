using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Models.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("post");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.AuthorId).HasColumnName("author_id");

            builder.Property(e => e.Body).HasColumnName("body");

            builder.Property(e => e.CreatedAt).HasColumnName("created_at");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasColumnName("title")
                .HasMaxLength(100);

            builder.HasOne(d => d.Author)
                .WithMany(p => p.Posts)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Post_AspNetUsers_AuthorId");
        }
    }
}