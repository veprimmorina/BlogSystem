using BlogSystem.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogSystem.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<BlogPost> BlogPosts { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Relationships
        builder.Entity<BlogPost>()
            .HasOne(b => b.Creator)
            .WithMany(u => u.BlogPosts)
            .HasForeignKey(b => b.CreatorId);

        builder.Entity<BlogPost>()
            .HasMany(b => b.Tags)
            .WithMany(t => t.BlogPosts)
            .UsingEntity(j => j.ToTable("BlogPostTags"));

        builder.Entity<Comment>()
            .HasOne(c => c.BlogPost)
            .WithMany(b => b.Comments)
            .HasForeignKey(c => c.BlogPostId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Comment>()
            .HasOne(c => c.ParentComment)
            .WithMany(c => c.Replies)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Like>()
            .HasOne(l => l.User)
            .WithMany(u => u.Likes)
            .HasForeignKey(l => l.UserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Like>()
            .HasOne(l => l.BlogPost)
            .WithMany(b => b.Likes)
            .HasForeignKey(l => l.BlogPostId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Notification>()
            .HasOne(n => n.User)
            .WithMany(u => u.Notifications)
            .HasForeignKey(n => n.UserId);
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
    }
}