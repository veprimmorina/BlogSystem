using AutoMapper;
using BlogSystem.Core.DTO;
using BlogSystem.Core.Interfaces.Repositories;
using BlogSystem.Core.Interfaces.Service;
using BlogSystem.Core.Models;
using FluentValidation;

namespace BlogSystem.Core.Services
{
    public class BlogPostService : IBlogService
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IValidator<BlogPostDto> _BlogPostDTOValidator;
        private readonly IMapper _mapper;

        public BlogPostService(IBlogPostRepository blogPostRepository, IValidator<BlogPostDto> BlogPostDTOValidator, IMapper mapper)
        {
            _blogPostRepository = blogPostRepository;
            _BlogPostDTOValidator = BlogPostDTOValidator;
            _mapper = mapper;
        }

        public async Task<string> CreateBlogPost(BlogPostDto blogPost)
        {
            ValidateBlogPost(blogPost);
            var blogPostDTO = _mapper.Map<BlogPostDto, BlogPost>(blogPost);
            await _blogPostRepository.Create(blogPostDTO);

            return "Created Succesfully";
        }

        public bool ValidateBlogPost(BlogPostDto createBlogPost)
        {
            var validationResult = _BlogPostDTOValidator.Validate(createBlogPost);

            if (!validationResult.IsValid)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteBlogPost(int postId)
        {
            var existingBlogPost = await _blogPostRepository.GetBlogPost(postId);

            if (existingBlogPost == null)
            {
                return false; 
            }

            await _blogPostRepository.DeleteBlogPost(existingBlogPost);

            return true;
        }

        public async Task<List<BlogPostDto>> FilterBlogPosts(DateTime? startDate, DateTime? endDate, List<string> tags)
        {
            var blogPosts = _blogPostRepository.GetBlogPostAsQueryable();

            if (startDate.HasValue)
            {
                blogPosts = blogPosts.Where(bp => bp.PublicationDate >= startDate);
            }
            if (endDate.HasValue)
            {
                blogPosts = blogPosts.Where(bp => bp.PublicationDate <= endDate);
            }

            if (tags != null && tags.Any())
            {
                blogPosts = blogPosts.Where(bp => bp.Tags.Any(tag => tags.Contains(tag.TagName)));
            }

            return _mapper.Map<List<BlogPostDto>>(blogPosts);
        }

        public async Task<List<BlogPostDto>> GetAllBlogPostsAsync()
        {
            var blogPosts = await _blogPostRepository.GetAllBlogPostsAsync();
            return _mapper.Map<List<BlogPostDto>>(blogPosts);
        }

        public async Task<BlogPost> GetBlogDataById(int id)
        {
           return await _blogPostRepository.GetBlogPostById(id);
        }

        public async Task<List<BlogPostDto>> SearchBlogPostsByTitle(string searchTerm)
        {
            var blogPosts = await _blogPostRepository.SearchBlogPostsByTitle(searchTerm);
            return _mapper.Map<List<BlogPostDto>>(blogPosts);
        }

        public async Task<bool> UpdateBlogPost(int postId, BlogPost updateDto)
        {
            var existingBlogPost = await _blogPostRepository.GetBlogPost(postId);

            if (existingBlogPost == null)
            {
                return false; 
            }

            existingBlogPost.Title = updateDto.Title;
            existingBlogPost.Content = updateDto.Content;

            await _blogPostRepository.UpdateBlogPost(existingBlogPost);
            return true;
        }
    }
}
