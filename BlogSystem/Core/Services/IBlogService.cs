using BlogSystem.Core.DTO;
using BlogSystem.Core.Models;

namespace BlogSystem.Core.Services
{
    public interface IBlogService
    {
        BlogPost CreateBlogPost(BlogPost blogPost);
        public  Task<List<BlogPostDto>> GetAllBlogPostsAsync();

        public  Task<bool> UpdateBlogPost(int postId, BlogPost updateDto);

        public Task<bool> DeleteBlogPost(int postId);

        public Task<List<BlogPostDto>> SearchBlogPostsByTitle(string searchTerm);

        public Task<List<BlogPostDto>> FilterBlogPosts(DateTime? startDate, DateTime? endDate, List<string> tags);

        public Task<BlogPost> GetBlogDataById(int id);
    }
}
