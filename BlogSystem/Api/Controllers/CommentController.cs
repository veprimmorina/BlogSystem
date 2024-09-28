using AutoMapper;
using BlogSystem.Core.DTO;
using BlogSystem.Core.Services;
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

        [HttpPost("comment")]
        public async Task<String> CreateComment([FromBody] CommentDTO commentDto)
        {

            if (commentDto == null)
            {
                return "BadRequest";
            }

            var createdComment = await _commentService.CreateComment(commentDto);

            return "Created";

        }

        [HttpGet("comments")]
        public async Task<IActionResult> GetAllComments()
        {

            var comments = await _commentService.GetAllComments();
            return Ok(comments);

        }

        [HttpPost("reply")]
        public async Task<String> AddReply([FromBody] CommentDTO replyDto)
        {

            if (replyDto == null)
            {
                return "Reply was not made";
            }

            var reply = await _commentService.AddReply(replyDto);
            return reply;

        }

    }

}
