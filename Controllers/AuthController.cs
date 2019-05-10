using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using angu.Data;
using angu.Dtos;
using angu.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace angu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRespository _auth;
        private readonly IConfiguration _config;

        public AuthController(IAuthRespository auth, IConfiguration configuration )
        {
            _auth = auth;
            _config = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userDto)
        {

            userDto.Username = userDto.Username.ToLower();
            if(await _auth.UserExist(userDto.Username)) return BadRequest("user is already exist with this username");

            var userToCreate  = new User {
                Username = userDto.Username,
                CreatedAt = userDto.CreatedAt,
                LastActive = userDto.LastActive,
                Gender = userDto.Gender,
                Photos = userDto.Photos
            };

            var createduser = await _auth.RegisterUser(userToCreate , userDto.Password);

            return StatusCode(201);
            
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            var userFromRepo = await _auth.Login(loginDto.Username, loginDto.Password);

            if(userFromRepo == null) return Unauthorized("invalid credentials");

            var claims = new []
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDiscriptor);  



            return Ok(
                new {
                    Token = tokenHandler.WriteToken(token)
                }
            );

        }

    }
}