using BlogSystem.Core.DTO;
using BlogSystem.Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginRequest)
    {
        var token = await _userService.LoginAsync(loginRequest);
        return Ok(new { token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel registerRequest)
    {
        var response = await _userService.RegisterUser(registerRequest);
        return Ok(response);
    }

    [HttpGet("checkUserRole")]
    public async Task<IActionResult> CheckUserRole(string userName, string role)
    {
        var response = await _userService.CheckUserRole(userName, role);
        return Ok(response);

    }

    [HttpGet("getUserById")]
    public async Task<IActionResult> GetUserById(string userId)
    {
        var response = await _userService.GetUserById(userId);
        return Ok(response);

    }

}
