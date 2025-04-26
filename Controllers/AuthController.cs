using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinyTitan.Habits.API.Auth;
using TinyTitan.Habits.API.Data;
using TinyTitan.Habits.API.DTOs;
using TinyTitan.Habits.API.Models;

namespace TinyTitan.Habits.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly AuthService _authService;

    public AuthController(AppDbContext dbContext, AuthService authService)
    {
        _dbContext = dbContext;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDto dto)
    {
        if (await _dbContext.Users.AnyAsync(u => u.Email == dto.Email))
        {
            return BadRequest("User already exists with that email.");
        }

        var user = new User
        {
            Username = dto.Name,
            Email = dto.Email,
            PasswordHash = _authService.HashPassword(dto.Password)
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        var token = _authService.GenerateJwtToken(user);

        return Ok(new { token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null || !_authService.VerifyPassword(dto.Password, user.PasswordHash))
        {
            return Unauthorized("Invalid credentials.");
        }

        var token = _authService.GenerateJwtToken(user);

        return Ok(new { token });
    }
}
