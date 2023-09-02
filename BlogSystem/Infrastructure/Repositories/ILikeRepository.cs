using BlogSystem.Core.Models;

namespace BlogSystem.Infrastructure.Repositories
{
    public interface ILikeRepository
    {
        public Task AddLike(Like like);

    }
}
