using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlogsWebApi.Models
{
    public partial class BlogWA2Context : DbContext
    {
        public BlogWA2Context()
        {
        }

        public BlogWA2Context(DbContextOptions<BlogWA2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostType> PostTypes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.IdComment)
                    .HasName("PK__Comments__57C9AD58EF493303");

                entity.Property(e => e.Comment1).HasColumnName("Comment");

                entity.HasOne(d => d.IdPostNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.IdPost)
                    .HasConstraintName("FK_Comments.IdPost")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_Comments.IdUser");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.IdPost)
                    .HasName("PK__Posts__F8DCBD4D380F76A9");

                entity.Property(e => e.PostPath)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_Posts.IdUser");

                entity.HasOne(d => d.PostTypeNavigation)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.PostType)
                    .HasConstraintName("FK_Posts.PostType");
            });

            modelBuilder.Entity<PostType>(entity =>
            {
                entity.HasKey(e => e.IdPostType)
                    .HasName("PK__PostType__7DACA547E64A8331");

                entity.ToTable("PostType");

                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__Users__B7C92638112DA417");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
