using Auth.Core.DTOs;
using Auth.Core.Interfaces;
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

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var tokenResponse = await _userService.RegisterAsync(request);
        return Ok(tokenResponse);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var tokenResponse = await _userService.LoginAsync(request);
        return Ok(tokenResponse);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var tokenResponse = await _userService.RefreshTokenAsync(request);
        return Ok(tokenResponse);
    }
}
