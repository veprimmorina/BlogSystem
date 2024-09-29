using BlogSystem.Areas.Identity.Data;
using BlogSystem.Core.Interfaces.Repositories;
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

        public async Task Create(BlogPost blogPost)
        {
            try
            {
                _dbContext.BlogPosts.Add(blogPost);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task DeleteBlogPost(BlogPost blogPost)
        {
            try
            {
                _dbContext.BlogPosts.Remove(blogPost);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public IQueryable<BlogPost> GetBlogPostAsQueryable()
        {
            try
            {
                var query = _dbContext.BlogPosts.AsQueryable();
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<List<BlogPost>> GetAllBlogPostsAsync()
        {
            try
            {
                return await _dbContext.BlogPosts.ToListAsync();
            }
            catch(Exception ex) 
            { 
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<BlogPost> GetBlogPost(int postId)
        {
            try
            {
                return await _dbContext.BlogPosts.FindAsync(postId);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<List<BlogPost>> SearchBlogPostsByTitle(string searchTerm)
        {
            try
            {
                return await _dbContext.BlogPosts
                            .Where(bp => bp.Title.Contains(searchTerm))
                            .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task UpdateBlogPost(BlogPost blogPost)
        {
            try
            {
                _dbContext.BlogPosts.Update(blogPost);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<BlogPost> GetBlogPostById(int id)
        {
            try
            {
                return await _dbContext.BlogPosts.FindAsync(id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

    }
}
