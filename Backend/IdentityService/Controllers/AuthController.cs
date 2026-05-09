using Microsoft.AspNetCore.Mvc;
using IdentityService.Data;
using IdentityService.Entities;
using IdentityService.DTOs;
using IdentityService.Services;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;

    public AuthController(AppDbContext context, ITokenService tokenService)
    {
        //this._context = context
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        // şifreyi hash'leme
        var hashedPw = PasswordHasher.HashPassword(dto.Password);

        //yeni kullanıcı nesnesi
        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = hashedPw,
            Role = dto.Role
        };

        // veri tabanına kaydet
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("Kullanıcı kaydedildi.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        // kullancıyı e-posta ile bulalım
        var user = await _context.Users
            .IgnoreQueryFilters() // giriş yaparken tenant filtresine takılmayalım
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null) return Unauthorized("Geçersiz e-posta veya şifre girdiniz.");

        // şifreyi doğrula
        var result = PasswordHasher.VerifyPassword(dto.Password, user.PasswordHash);

        if (!result)
            return Unauthorized("Geçersiz e-posta veya şifre girdiniz.");

        //token üret ve bitir.
        var token = _tokenService.CreateToken(user);

        return Ok(new { token });
    }

    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("Gateway üzerinden IdentityService'e ulaştık.");    }
}
