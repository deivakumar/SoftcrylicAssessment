using HospitalAssessment.Shared.Patients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospitalAssessment.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<PatientsController> _logger;
        private readonly IConfiguration _config;

        public AuthController(ILogger<PatientsController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpPost]
        public IActionResult GetToken([FromBody]LoginCommand command)
        {
            if (command == null || string.IsNullOrEmpty(command.Username))
                return BadRequest("Username required"); ;

            string token = string.Empty;
            try
            {
               token = CreateToken(command.Username);
                if (string.IsNullOrEmpty(token))
                    return BadRequest("Request failed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return Ok(token);
        }

        [NonAction]
        private string CreateToken(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userName),
                new Claim(ClaimTypes.Name, userName),
                new Claim("ClientId", "")
            };

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            int expiryMins = Convert.ToInt32(_config["Jwt:ExpiryMins"]);
            var creds = new SigningCredentials(authKey, SecurityAlgorithms.HmacSha512Signature);
            var secToken = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(expiryMins), signingCredentials: creds, audience: audience, issuer: issuer);

            return new JwtSecurityTokenHandler().WriteToken(secToken);
        }
    }
}
