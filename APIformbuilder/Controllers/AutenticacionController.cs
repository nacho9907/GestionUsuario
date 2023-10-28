using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIformbuilder.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
namespace APIformbuilder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly string secretKey;
        public AutenticacionController(IConfiguration config)
        {
             secretKey = config.GetSection("settings").GetSection("secretkey").ToString();
        }
        [HttpPost]
        [Route("validar")]
        public ActionResult validar ([FromBody] LogUsuario request) {
            if(request.Username == "@Username" && request.Password == "@Password")
            {
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.Username));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
                string tokenCreado = tokenHandler.WriteToken(tokenConfig);
                return StatusCode(StatusCodes.Status200OK, new { token = tokenCreado });
               
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "El usuario es incorrecto o no existe" });
            }
        }
    }
}
