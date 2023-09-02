using BlogSystem.Areas.Identity.Data;
using BlogSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Infrastructure.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BlogPost Create(BlogPost blogPost)
        {
            _dbContext.BlogPosts.Add(blogPost);
            _dbContext.SaveChanges();
            return blogPost;
        }

        public async Task DeleteBlogPost(BlogPost blogPost)
        {
            _dbContext.BlogPosts.Remove(blogPost);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<BlogPost>> FilterBlogPosts(DateTime? startDate, DateTime? endDate, List<string> tags)
        {
            var query = _dbContext.BlogPosts.AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(bp => bp.PublicationDate >= startDate);
            }

            if (endDate.HasValue)
            {
                query = query.Where(bp => bp.PublicationDate <= endDate);
            }

            if (tags != null && tags.Any())
            {
                query = query.Where(bp => bp.Tags.Any(tag => tags.Contains(tag.TagName)));
            }

            return await query.ToListAsync();
        }

        public async Task<List<BlogPost>> GetAllBlogPostsAsync()
        {
            return await _dbContext.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost> GetBlogPost(int postId)
        {
            return await _dbContext.BlogPosts.FindAsync(postId);
        }

        public async Task<List<BlogPost>> SearchBlogPostsByTitle(string searchTerm)
        {
            return await _dbContext.BlogPosts
                .Where(bp => bp.Title.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task UpdateBlogPost(BlogPost blogPost)
        {
            _dbContext.BlogPosts.Update(blogPost);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BlogPost> GetBlogPostById(int id)
        {
            return await _dbContext.BlogPosts.FindAsync(id);
        }

    }
}
