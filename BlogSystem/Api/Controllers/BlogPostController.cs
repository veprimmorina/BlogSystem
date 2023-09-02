using AutoMapper;
using BlogSystem.Api.DTO;
using BlogSystem.Core.Models;
using BlogSystem.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogService _blogPostService;
        private readonly ILikeService _likeService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public BlogPostsController(IBlogService blogPostService, IMapper mapper, ILikeService likeService, IUserService userService)
        {

            _blogPostService = blogPostService;
            _likeService = likeService;
            _userService = userService;
            _mapper = mapper;

        }

        [Authorize(Roles = "Administrator,Creator")]
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] BlogPostDTO blogDto)
        {

            var blogPost = _mapper.Map<BlogPostDTO, BlogPost>(blogDto);

            var createdBlogPost = _blogPostService.CreateBlogPost(blogPost);

            var responseDto = _mapper.Map<BlogPost, BlogPostDTO>(createdBlogPost);

            return Ok("Success");

        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {

            var blogPosts = await _blogPostService.GetAllBlogPostsAsync();
            return Ok(blogPosts);

        }

        [Authorize(Roles = "Administrator,Creator")]
        [HttpPut("{postId}")]
        public async Task<IActionResult> UpdateBlogPost(int postId, [FromBody] BlogPost updateDto)
        {

            var success = await _blogPostService.UpdateBlogPost(postId, updateDto);

            if (success)
            {
                return NoContent(); 
            }

            return NotFound(); 

        }

        [Authorize(Roles = "Administrator,Creator")]
        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeleteBlogPost(int postId)
        {

            var success = await _blogPostService.DeleteBlogPost(postId);

            if (success)
            {
                return NoContent(); 
            }

            return NotFound(); 

        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public async Task<BlogPost> GetBlogPost(int id)
        {

            return await _blogPostService.GetBlogDataById(id);

        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBlogPostsByName([FromQuery] string searchTerm)
        {

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Search term cannot be empty.");
            }

            var searchResults = await _blogPostService.SearchBlogPostsByTitle(searchTerm);

            if (searchResults.Count == 0)
            {
                return NotFound("No matching blog posts found.");
            }

            return Ok(searchResults);

        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterBlogPosts([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] List<string> tags)
        {

            var filteredBlogPosts = await _blogPostService.FilterBlogPosts(startDate, endDate, tags);

            if (filteredBlogPosts.Count == 0)
            {
                return NotFound("No matching blog posts found.");
            }

            return Ok(filteredBlogPosts);

        }
        

        [HttpGet("like")]
        public async Task<String> likeBlogPost(int postId)
        {

            var token = _userService.getTokenFromAuthorizationHeader(HttpContext);
            var userId = await _userService.GetUserIdFromToken(token);

            if (userId == null)
            {
                return "Log in first";
            }

            _likeService.addLike(userId, postId);

            return "Liked";

        }
       
    }
}
