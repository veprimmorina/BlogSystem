using AutoMapper;
using BlogSystem.Api.DTO;
using BlogSystem.Core.Models;
using BlogSystem.Infrastructure.Repositories;
using FluentValidation;

namespace BlogSystem.Core.Services
{
    public class BlogPostService : IBlogService
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IValidator<BlogPostDTO> _BlogPostDTOValidator;
        private readonly IMapper _mapper;

        public BlogPostService(IBlogPostRepository blogPostRepository, IValidator<BlogPostDTO> BlogPostDTOValidator, IMapper mapper)
        {

            _blogPostRepository = blogPostRepository;
            _BlogPostDTOValidator = BlogPostDTOValidator;
            _mapper = mapper;

        }

        public BlogPost CreateBlogPost(BlogPost blogPost)
        {

            var BlogPostDTO = _mapper.Map<BlogPost, BlogPostDTO>(blogPost);
            validateBlogPost(BlogPostDTO);
            return _blogPostRepository.Create(blogPost);

        }

        public bool validateBlogPost(BlogPostDTO BlogPostDTO)
        {

            var validationResult = _BlogPostDTOValidator.Validate(BlogPostDTO);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
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

        public async Task<List<BlogPostDTO>> FilterBlogPosts(DateTime? startDate, DateTime? endDate, List<string> tags)
        {

            var blogPosts = await _blogPostRepository.FilterBlogPosts(startDate, endDate, tags);
            return _mapper.Map<List<BlogPostDTO>>(blogPosts);

        }

        public async Task<List<BlogPostDTO>> GetAllBlogPostsAsync()
        {

            var blogPosts = await _blogPostRepository.GetAllBlogPostsAsync();
            return _mapper.Map<List<BlogPostDTO>>(blogPosts);

        }

        public async Task<BlogPost> GetBlogDataById(int id)
        {

           return await _blogPostRepository.GetBlogPostById(id);

        }

        public async Task<List<BlogPostDTO>> SearchBlogPostsByTitle(string searchTerm)
        {

            var blogPosts = await _blogPostRepository.SearchBlogPostsByTitle(searchTerm);
            return _mapper.Map<List<BlogPostDTO>>(blogPosts);

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
