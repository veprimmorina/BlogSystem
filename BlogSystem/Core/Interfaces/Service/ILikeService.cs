namespace BlogSystem.Core.Interfaces.Service
{
    public interface ILikeService
    {
        public Task AddLike(int blogPost);
    }
}
