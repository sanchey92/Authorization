using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Authorization.JwtBearer.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Sub, "Alexandr"),
                new (JwtRegisteredClaimNames.Email, "alex@gmail.com")
            };

            var secretBytes = Encoding.UTF8.GetBytes(Constants.SecretKey);
            var key = new SymmetricSecurityKey(secretBytes);
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                Constants.Issuer,
                Constants.Audience,
                claims,
                DateTime.Now,
                DateTime.Now.AddMinutes(60),
                signingCredentials);

            var value = new JwtSecurityTokenHandler().WriteToken(token);
            ViewBag.Token = value;
            
            return View();
        }
    }
}