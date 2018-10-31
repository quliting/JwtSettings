using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtSettings.Model;
using JwtSettings.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JwtSettings.Controllers
{
    [Route("api/[controller]")]
    public class AuthorizatonController : Controller
    {
        private readonly JwtSetting _jwtSetting;

        public AuthorizatonController(IOptions<JwtSetting> jwtSetting)
        {
            _jwtSetting = jwtSetting.Value;
        }
        [HttpPost]
        public IActionResult Token([FromBody] LoginViewModel loginViewModel)
        { 
            if (!ModelState.IsValid) return BadRequest();
            if (loginViewModel.UserName != "qlt" && loginViewModel.Role != "admin") return BadRequest();

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "qlt"),
                new Claim(ClaimTypes.Role, "admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecretKey));
            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_jwtSetting.Issuer,_jwtSetting.Audience,claims,DateTime.Now,DateTime.Now.AddMinutes(30),creds);
            return Ok(new {token=new JwtSecurityTokenHandler().WriteToken(token)});
        }
    }
}