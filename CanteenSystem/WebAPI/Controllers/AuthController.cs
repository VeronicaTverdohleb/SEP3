using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Shared.Dtos;
using Shared.Model;

namespace WebAPI.Controllers;

/// <summary>
/// Web API method definition related to Authentication functionality
/// </summary>
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration config;
    private readonly IAuthService authService;

    public AuthController(IConfiguration config, IAuthService authService)
    {
        this.config = config;
        this.authService = authService;
    }


    /// <summary>
    /// Method that takes a User and generates Claims for them (which is something the program understand)
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private List<Claim> GenerateClaims(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim("Id", user.Id.ToString()),
            new Claim("Username", user.UserName),
            new Claim("Role", user.Role),
            new Claim("Firstname", user.FirstName),
            new Claim("Lastname", user.LastName),
            new Claim("Password", user.Password),
            new Claim("Email", user.Email)
        };
        return claims.ToList();
    }

    /// <summary>
    /// This method generates a JWT to be returned to the User trying to log in 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private string GenerateJwt(User user)
    {
        List<Claim> claims = GenerateClaims(user);

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        JwtHeader header = new JwtHeader(signIn);

        JwtPayload payload = new JwtPayload(
            config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims,
            null,
            DateTime.UtcNow.AddMinutes(60));

        JwtSecurityToken token = new JwtSecurityToken(header, payload);

        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }
    
    /// <summary>
    /// POST endpoint that calls the AuthService and GenerateJwt for the User
    /// </summary>
    /// <param name="userLoginDto"></param>
    /// <returns></returns>
    [HttpPost, Route("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        try
        {
            User user = await authService.ValidateUser(userLoginDto.Username, userLoginDto.Password);
            string token = GenerateJwt(user);
    
            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}

