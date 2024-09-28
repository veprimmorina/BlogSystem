using BlogSystem.Core.DTO;
using BlogSystem.Core.Models;

namespace BlogSystem.Core.Services
{
    public interface ICommentService
    {

        public Task<CommentDTO> CreateComment(CommentDTO CommentDTO);

        public Task<IEnumerable<CommentDTO>> GetAllComments();

        public Task<String> AddReply(CommentDTO replyDto);

        public Task<Comment> getCommentById(int? commentId);

    }
}
