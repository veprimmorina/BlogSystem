using BlogSystem.Core.Models;

namespace BlogSystem.Infrastructure.Repositories
{
    public interface ICommentRepository
    {

        public Task<IEnumerable<Comment>> GetAllComments();

        public Task<Comment> Add(Comment comment);

        public Task<Comment> GetById(int? id);

        Task<bool> checkIfExists(int? parentCommentId);
    }
}
