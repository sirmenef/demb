using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Demb.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;

        public TokenController(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]Login login)
        {
            IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<string> CreateUser([FromBody]User newUser)
        {

            if (ModelState.IsValid)
            {
                await _context.user.AddAsync(newUser);
                await _context.SaveChangesAsync();
                return "\" USer Created \"";
            }

            return null;


        }

        private User Authenticate(Login login)
        {
            User user = null;
            var model = _context.user.FirstOrDefault(x => x.Username == login.Username);
            if (login.Username == model.Username && login.Password == model.Password)
            {
                user = model;
            }
            return user;
        }

        private string BuildToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}