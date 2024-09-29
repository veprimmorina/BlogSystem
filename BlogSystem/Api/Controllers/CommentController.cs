using BlogSystem.Core.DTO;
using BlogSystem.Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.Api.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("createComment")]
        public async Task<IActionResult> CreateComment([FromBody] CommentDto commentDto)
        {
            var response = await _commentService.CreateComment(commentDto);
            return Ok(response);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllComments()
        {
            var response = await _commentService.GetAllComments();
            return Ok(response);
        }

        [HttpPost("reply")]
        public async Task<IActionResult> AddReply([FromBody] CommentDto replyDto)
        {
            var response = await _commentService.AddReply(replyDto);
            return Ok(response);
        }
    }

}
