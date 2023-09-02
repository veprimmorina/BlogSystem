using BlogSystem.Core.Models;

namespace BlogSystem.Infrastructure.Repositories
{
    public interface IBlogPostRepository
    {
        BlogPost Create(BlogPost blogPost);

        public Task<List<BlogPost>> GetAllBlogPostsAsync();

        public Task UpdateBlogPost(BlogPost blogPostDto);

        public Task<BlogPost> GetBlogPost(int postId);

        public Task DeleteBlogPost(BlogPost blogPost);

        public Task<List<BlogPost>> SearchBlogPostsByTitle(string searchTerm);

        public Task<List<BlogPost>> FilterBlogPosts(DateTime? startDate, DateTime? endDate, List<string> tags);

        public Task<BlogPost> GetBlogPostById(int id);

    }
}
