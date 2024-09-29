using BlogSystem.Core.DTO;
using BlogSystem.Core.Interfaces.Service;
using BlogSystem.Core.Models;
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

        public BlogPostsController(IBlogService blogPostService, ILikeService likeService)
        {
            _blogPostService = blogPostService;
            _likeService = likeService;
        }

        [Authorize(Roles = "Administrator,Creator")]
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] BlogPostDto blogDto)
        {
            var response = await _blogPostService.CreateBlogPost(blogDto);
            return Ok(response);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var response = await _blogPostService.GetAllBlogPostsAsync();
            return Ok(response);
        }

        [Authorize(Roles = "Administrator,Creator")]
        [HttpPut("{postId}")]
        public async Task<IActionResult> UpdateBlogPost(int postId, [FromBody] BlogPost updateDto)
        {
            var response = await _blogPostService.UpdateBlogPost(postId, updateDto);
            return Ok(response); 
        }

        [Authorize(Roles = "Administrator,Creator")]
        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeleteBlogPost(int postId)
        {
            var response = await _blogPostService.DeleteBlogPost(postId);
            return Ok(response); 
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public async Task<BlogPost> GetBlogPost(int id)
        {
            var response = await _blogPostService.GetBlogDataById(id);
            return response;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBlogPostsByName([FromQuery] string searchTerm)
        {
            var response = await _blogPostService.SearchBlogPostsByTitle(searchTerm);
            return Ok(response);

        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterBlogPosts([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] List<string> tags)
        {
            var response = await _blogPostService.FilterBlogPosts(startDate, endDate, tags);
            return Ok(response);
        }

        [HttpGet("like")]
        public async Task<IActionResult> LikeBlogPost(int postId)
        {
            var response = _likeService.AddLike(postId);
            return Ok(response);
        }
       
    }
}
