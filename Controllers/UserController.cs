using AutoMapper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjeIhale.Data;
using ProjeIhale.Dtos;
using ProjeIhale.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace ProjeIhale.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private IAuthRepository _authRepository;
        private IConfiguration _configuration;
        private DataContext _context;

        public UserController(IAuthRepository authRepository, IConfiguration configuration, DataContext context)
        {
            _authRepository = authRepository;
            _configuration = configuration;
            _context = context;
        }
        [HttpPost("userregister")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            if (await _authRepository.UserExists(userForRegisterDto.UserName))
            {
                ModelState.AddModelError("UserName", "Username already exist");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userToCreate = new User
            {
                UserName = userForRegisterDto.UserName,
                Name=userForRegisterDto.Name,
                LastName=userForRegisterDto.LastName
            };
            var createdUser = await _authRepository.RegisterUser(userToCreate, userForRegisterDto.Password);
            return StatusCode(201); //oluşturuldu uyarısı
        }
        [HttpPost("changePassUser")]
        public async Task<IActionResult> ChangePassUser([FromBody] UserForLoginDto userForLoginDto)
        {
            if (await _authRepository.UserExists(userForLoginDto.UserName))
            {
                var user = _context.Users.Where(p => p.UserName == userForLoginDto.UserName).FirstOrDefault();
                var change = await _authRepository.ChangePassUser(user, userForLoginDto.Password);
                return StatusCode(201);
            } 
            return BadRequest(ModelState);

        }

        [HttpPost("userlogin")]
        public async Task<ActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            var user = await _authRepository.LoginUser(userForLoginDto.UserName, userForLoginDto.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
                    , SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(tokenString);

        }


    }
}
