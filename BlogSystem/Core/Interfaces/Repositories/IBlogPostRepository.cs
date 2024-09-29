using BlogSystem.Core.Models;

namespace BlogSystem.Core.Interfaces.Repositories
{
    public interface IBlogPostRepository
    {
        public Task Create(BlogPost blogPost);

        public Task<List<BlogPost>> GetAllBlogPostsAsync();

        public Task UpdateBlogPost(BlogPost blogPostDto);

        public Task<BlogPost> GetBlogPost(int postId);

        public Task DeleteBlogPost(BlogPost blogPost);

        public Task<List<BlogPost>> SearchBlogPostsByTitle(string searchTerm);

        public IQueryable<BlogPost> GetBlogPostAsQueryable();

        public Task<BlogPost> GetBlogPostById(int id);

    }
}
