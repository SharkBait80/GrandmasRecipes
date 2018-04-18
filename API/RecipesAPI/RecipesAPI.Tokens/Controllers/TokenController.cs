using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Security;

namespace RecipesAPI.Tokens.Controllers
{
    [Produces("application/json")]
    [Route("api/Token")]
    public class TokenController : Controller
    {

        [Microsoft.AspNetCore.Mvc.HttpPost()]
        [AllowAnonymous]
        public IActionResult Get([Microsoft.AspNetCore.Mvc.FromForm]string password)
        {
            if (CheckUser(password))
            {
                var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, SecurityConstants.ClaimName)
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityConstants.SecurityKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var expiry = DateTime.Now.AddMinutes(1);

                var token = new JwtSecurityToken(
                    issuer: SecurityConstants.Issuer,
                    audience: SecurityConstants.Issuer,
                    claims: claims,
                    expires: expiry,
                    signingCredentials: creds);

                return Ok(new
                {
                   // TODO: return some value
                });
            }

            return BadRequest("Could not verify username and password");
        }

        public bool CheckUser(string password)
        {
            // Should do a more sophisticated check for a real website
            return password == SecurityConstants.Password;
        }
    }
}