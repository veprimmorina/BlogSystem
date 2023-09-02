using BlogSystem.Core.Models;

namespace BlogSystem.Core.Services
{
    public interface ILikeService
    {

        public Task addLike(string userId, int blogPost);

    }
}
