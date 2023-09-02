using BlogSystem.Api.DTO;
using BlogSystem.Core.Models;

namespace BlogSystem.Core.Services
{
    public interface IBlogService
    {
        BlogPost CreateBlogPost(BlogPost blogPost);
        public  Task<List<BlogPostDTO>> GetAllBlogPostsAsync();

        public  Task<bool> UpdateBlogPost(int postId, BlogPost updateDto);

        public Task<bool> DeleteBlogPost(int postId);

        public Task<List<BlogPostDTO>> SearchBlogPostsByTitle(string searchTerm);

        public Task<List<BlogPostDTO>> FilterBlogPosts(DateTime? startDate, DateTime? endDate, List<string> tags);

        public Task<BlogPost> GetBlogDataById(int id);
    }
}
