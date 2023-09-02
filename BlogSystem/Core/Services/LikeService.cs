using BlogSystem.Core.Models;
using BlogSystem.Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogSystem.Core.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;

        public LikeService(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }


        public async Task addLike(string userId, int blogPost)
        {

            var like = new Like
            {
                UserId = userId,
                BlogPostId = blogPost
            };

            await _likeRepository.AddLike(like);

        }


    }
}
