using BlogSystem.Core.DTO;
namespace BlogSystem.Core.Interfaces.Service
{
    public interface IUserService
    {
        public Task<string> LoginAsync(LoginModel loginRequest);
        public Task<string> GetUserEmailById(string userId);
        public Task<string> GetUserIdFromToken(string token);
        public string GetTokenFromAuthorizationHeader(HttpContext context);
        public Task<UserDto> GetUserById(string userId);
        public Task<string> CheckUserRole(string userName, string role);
        public Task<string> RegisterUser(RegisterModel registerRequest);
    }
}
