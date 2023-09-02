using AutoMapper;
using BlogSystem.Api.DTO;
using BlogSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogSystem.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {

            _userManager = userManager;
            _mapper = mapper;

        }

        public UserService()
        {
        }

        public async Task<String> GetUserEmailById(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var userDto = new UserDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return userDto.Email;

        }

        public string getTokenFromAuthorizationHeader(HttpContext context)
        {

            string authorizationHeader = context.Request.Headers["Authorization"];

            if (string.IsNullOrWhiteSpace(authorizationHeader))
            {
                return null;
            }

            if (!authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            string token = authorizationHeader.Substring("Bearer ".Length).Trim();

            return token;

        }

        public async Task<string> GetUserIdFromToken(string token)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));

            try
            {
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out validatedToken);

                var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                
                if (userIdClaim != null)
                {
                    return await Task.FromResult(userIdClaim.Value);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
