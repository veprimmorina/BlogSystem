using BlogSystem.Core.DTO;
using BlogSystem.Core.Models;

namespace BlogSystem.Core.Interfaces.Service
{
    public interface ICommentService
    {

        public Task<CommentDto> CreateComment(CommentDto CommentDTO);

        public Task<IEnumerable<CommentDto>> GetAllComments();

        public Task<string> AddReply(CommentDto replyDto);

        public Task<Comment> GetCommentById(int? commentId);

    }
}
