using BlogSystem.Areas.Identity.Data;
using BlogSystem.Core.Interfaces.Repositories;
using BlogSystem.Core.Models;

namespace BlogSystem.Infrastructure.Repositories
{
    public class LikeRepository : ILikeRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public LikeRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task AddLike(Like like)
        {
            try
            {
                _dbContext.Likes.Add(like);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
