using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DotNetApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly MyDbContext _context;

    public AuthController(IConfiguration configuration, MyDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserCredentials credentials)
    {
        if (await _context.Users.AnyAsync(user => user.Username == credentials.Username))
        {
            return BadRequest("User already exists.");
        }

        CreatePasswordHash(credentials.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = new UserModel
        {
            Username = credentials.Username,
            PasswordHash = passwordHash,
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Ok("User registered successfully.");
    }

    [HttpPost("token")]
    public async Task<IActionResult> GenerateToken([FromBody] UserCredentials credentials)
    {
        if (!await ValidateUser(credentials))
        {
            return Unauthorized();
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, credentials.Username),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(21),
            signingCredentials: creds);

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }

    private async Task<bool> ValidateUser(UserCredentials credentials)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Username == credentials.Username);
        if (user == null) return false;

        byte[] passwordBytes = Encoding.UTF8.GetBytes(credentials.Password); ;

        return VerifyPasswordHash(credentials.Password, user.PasswordHash, passwordBytes);
    }
    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
        using (var hmac = new HMACSHA512(storedSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(storedHash);
        }
    }
}