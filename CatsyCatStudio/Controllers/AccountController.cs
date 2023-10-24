using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CatsyCatStudio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAccessToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult GetAccessToken()
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim> { new Claim("id", Guid.NewGuid().ToString()) };
                var tokenConfig = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"], audience: _configuration["Jwt:Audience"], claims: claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: credentials);
                string token = new JwtSecurityTokenHandler().WriteToken(tokenConfig);
                return Ok(new { AccessToken = $"bearer {token}" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred getting token. Error : {ex.Message}");
            }
        }
    }
}
