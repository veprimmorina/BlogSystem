using BlogSystem.Core.Interfaces.Repositories;
using BlogSystem.Core.Interfaces.Service;
using BlogSystem.Core.Models;

namespace BlogSystem.Core.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;

        public LikeService(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public async Task AddLike(int blogPost)
        {
            var like = new Like
            {
                BlogPostId = blogPost
            };

            await _likeRepository.AddLike(like);
        }
    }
}
