using BlogSystem.Api.DTO;
using BlogSystem.Areas.Identity.Data;
using BlogSystem.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;


    public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {

        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;

    }

    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
    }



    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {

        var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            var token = JwtTokenUtils.GenerateToken(user.Id, user.UserName);
            return Ok(new { token });
        }

        return Unauthorized();

    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {

        var user = new ApplicationUser
        {
            UserName = model.UserName,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            var roleExists = await _roleManager.RoleExistsAsync(model.Role);

            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(model.Role));
            }

            await _userManager.AddToRoleAsync(user, model.Role);

            return Ok(user.Id);
        }


        return BadRequest(result.Errors);

    }

    [HttpGet("checkUserRole")]
    public async Task<IActionResult> CheckUserRole(string userName, string role)
    {

        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        if (userRoles.Contains(role))
        {
            return Ok("User is associated with " + role);
        }
        else
        {
            return Ok("User is not associated with the " + role);
        }

    }

    [HttpGet("getUserById")]
    public async Task<IActionResult> GetUserById(string userId)
    {

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        var userDto = new UserDTO
        {
            UserName = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName
        };

        return Ok(userDto);

    }

}
