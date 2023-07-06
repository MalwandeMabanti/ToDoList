using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoList.Data;
using ToDoList.Models;


[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<ToDoUser> _userManager;
    private readonly SignInManager<ToDoUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(UserManager<ToDoUser> userManager, SignInManager<ToDoUser> signInManager, IConfiguration configuration, ILogger<AuthenticationController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _logger = logger;
    }

    // POST api/authentication/register
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] Register model)
    {
        var user = new ToDoUser { 
            UserName = model.Email, 
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,  
        };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return Ok(new {token = GenerateJsonWebToken(user)});
        }
        else
        {
            return BadRequest(result.Errors);
        }
    }

    // POST api/authentication/login
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] Login model)
    {
        var user = await _userManager.FindByNameAsync(model.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var token = GenerateJsonWebToken(user);

            _logger.LogInformation($"Token before sending to client: {token}");
            

            return Ok(new { Token = token});
        }
        return Unauthorized();
    }

    private string GenerateJsonWebToken(ToDoUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}