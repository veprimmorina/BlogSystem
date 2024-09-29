using BlogSystem.Core.Models;

namespace BlogSystem.Core.Interfaces.Repositories
{
    public interface ILikeRepository
    {
        public Task AddLike(Like like);

    }
}
