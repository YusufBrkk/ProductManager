using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AuthService.Core.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using AuthService.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly AuthDbContext _context;

    public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, AuthDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
            return Ok("Kayıt başarılı!");
        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var token = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            // Refresh token’ı veritabanına kaydet
            _context.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            });
            await _context.SaveChangesAsync();

            return Ok(new { token, refreshToken });
        }
        return Unauthorized("Kullanıcı adı veya şifre yanlış!");
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshRequestDto model)
    {
        var refreshToken = await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == model.RefreshToken && !rt.IsRevoked);

        if (refreshToken == null || refreshToken.ExpiryDate < DateTime.UtcNow)
            return Unauthorized("Geçersiz veya süresi dolmuş refresh token!");

        // Yeni JWT ve refresh token üret
        var newJwt = GenerateJwtToken(refreshToken.User);
        var newRefreshToken = GenerateRefreshToken();

        // Eski token’ı revoke et
        refreshToken.IsRevoked = true;

        // Yeni refresh token’ı kaydet
        var newTokenEntity = new RefreshToken
        {
            Token = newRefreshToken,
            UserId = refreshToken.UserId,
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };
        _context.RefreshTokens.Add(newTokenEntity);
        await _context.SaveChangesAsync();

        return Ok(new { token = newJwt, refreshToken = newRefreshToken });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user == null)
            return NotFound("Kullanıcı bulunamadı!");

        var result = await _userManager.AddToRoleAsync(user, model.Role);
        if (result.Succeeded)
            return Ok("Rol atandı!");
        return BadRequest(result.Errors);
    }

    [HttpPost("create-admin-role")]
    public async Task<IActionResult> CreateAdminRole()
    {
        var roleExists = await _roleManager.RoleExistsAsync("Admin");
        if (!roleExists)
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            return Ok("Admin rolü oluşturuldu.");
        }
        return BadRequest("Admin rolü zaten mevcut.");
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim("age", "22") // örnek özel claim
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }

    [Authorize(Policy = "RequireGmail")]
    [HttpGet("gmail-only")]
    public IActionResult GmailOnlyEndpoint()
    {
        return Ok("Sadece belirli e-posta ile giriş yapanlar görebilir!");
    }

    [Authorize(Policy = "Over18")]
    [HttpGet("adults-only")]
    public IActionResult AdultsOnlyEndpoint()
    {
        return Ok("18 yaşından büyük kullanıcılar görebilir!");
    }
}

// DTO'lar:
public class RegisterDto
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginDto
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; }
}

// DTO
public class RefreshRequestDto
{
    public string RefreshToken { get; set; }
}

public class AssignRoleDto
{
    public string UserName { get; set; }
    public string Role { get; set; }
}