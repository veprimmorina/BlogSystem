namespace BlogSystem.Core.Services
{
    public interface IUserService
    {

        public Task<String> GetUserEmailById(String userId);

        public Task<string> GetUserIdFromToken(string token);

        public string getTokenFromAuthorizationHeader(HttpContext context);

    }
}
