using LawyerAssistant.Application.DTOs.Identities;
using LawyerAssistant.Application.Features.Identities.Users.Commands;
using LawyerAssistant.Application.Objects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LawyerAssistant.PanelAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IOptions<AppConfig> _appConfig;

    public AccountController(ISender sender, IOptions<AppConfig> appConfig)
    {
        _sender = sender;
        _appConfig = appConfig;
    }


    /// <summary>
    /// Admin User Login Method (Call this method to receive a JWT token and gain access to the Admin Panel).
    /// </summary>
    /// <param name="command">Login credentials containing the admin's username and password.</param>
    /// <returns>Returns a JWT token if login is successful.</returns>
    /// <response code="200">Login successful. Returns a JWT token and admin user details.</response>
    /// <response code="400">Bad request. Validation errors or incorrect credentials.</response>
    /// <remarks>
    /// <b>Usage:</b><br/>
    /// This endpoint is used by Admin users to authenticate and receive a JWT token to access secure parts of the Admin Panel.<br/>
    /// <br/>
    /// <b>Request Example:</b>
    /// <code>
    /// {
    ///     "userName": "admin",
    ///     "password": "yourSecurePassword"
    /// }
    /// </code>
    /// <br/>
    /// <b>Response Example (200 OK):</b>
    /// <code>
    /// {
    ///     "isSuccess": true,
    ///     "message": "Login successful",
    ///     "value": {
    ///         "userId": 1,
    ///         "userName": "admin",
    ///         "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    ///     }
    /// }
    /// </code>
    /// </remarks>

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] LoginDTO command)
    {
        var result = await _sender.Send(new LoginCommand() { Dto = command });
        if (!result.IsSuccess) return Ok(result);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appConfig.Value.jwtTokenKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                        new Claim(ClaimTypes.Name , result.Value.UserName) ,
                        new Claim(ClaimTypes.NameIdentifier , result.Value.UserId.ToString())
            }),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        result.Value.Token = tokenHandler.WriteToken(token);
        return Ok(result);
    }
}
