using BlogSystem.Areas.Identity.Data;
using BlogSystem.Core.DTO;
using BlogSystem.Core.Interfaces.Service;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<string> LoginAsync(LoginModel loginRequest)
        {
            var result = await _signInManager.PasswordSignInAsync(loginRequest.UserName, loginRequest.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(loginRequest.UserName);
                return JwtTokenUtils.GenerateToken(user.Id, user.UserName);
            }

            throw new UnauthorizedAccessException("Invalid login attempt.");
        }

        public async Task<string> GetUserEmailById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            return user.Email;
        }

        public async Task<UserDto> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var response = new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return response;
        }

        public string GetTokenFromAuthorizationHeader(HttpContext context)
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

        public async Task<string> CheckUserRole(string userName, string role)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return "User not found.";
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Contains(role))
            {
                return "User is associated with " + role;
            }
            else
            {
                return "User is not associated with the " + role;
            }
        }

        public async Task<string> RegisterUser(RegisterModel registerRequest)
        {
            var user = new ApplicationUser
            {
                UserName = registerRequest.UserName,
                Email = registerRequest.Email,
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName
            };

            await _userManager.CreateAsync(user, registerRequest.Password);
            var roleExists = await _roleManager.RoleExistsAsync(registerRequest.Role);

            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(registerRequest.Role));
            }

            await _userManager.AddToRoleAsync(user, registerRequest.Role);

            return user.Id;
        }
    }
}
