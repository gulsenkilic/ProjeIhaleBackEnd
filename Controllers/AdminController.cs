using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; //
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration; //
using Microsoft.IdentityModel.Tokens;//
using ProjeIhale.Data;//
using ProjeIhale.Dtos;//
using ProjeIhale.Models;//
using System;//
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;//
using System.Linq;
using System.Security.Claims;//
using System.Text;//
using System.Threading.Tasks;//

namespace ProjeIhale.Controllers
{
    [Produces("application/json")]
    [Route("api/Admin")]
    
    public class AdminController : Controller
    {
        private IAuthRepository _authRepository;
        private IConfiguration _configuration;
        private DataContext _context;
        public AdminController(IAuthRepository authRepository, IConfiguration configuration , DataContext context)
        {
            _authRepository = authRepository;
            _configuration = configuration;
            _context = context;
        }


        [HttpPost("adminregister")]
        public async Task<IActionResult> Register([FromBody] AdminForRegisterDto adminForRegisterDto)
        {
            if (await _authRepository.AdminExists(adminForRegisterDto.UserName))
            {
                ModelState.AddModelError("UserName", "Username already exist");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var adminToCreate = new Admin
            {
                UserName = adminForRegisterDto.UserName,
                Name = adminForRegisterDto.Name,
                LastName = adminForRegisterDto.LastName
            };

            var createdAdmin = await _authRepository.RegisterAdmin(adminToCreate, adminForRegisterDto.Password);
            return StatusCode(201);
        }
        [HttpPost("changePassAdmin")]
        public async Task<IActionResult> ChangePassAdmin([FromBody] AdminForLoginDto adminForLoginDto)
        {
            if (await _authRepository.AdminExists(adminForLoginDto.UserName))
            {
                var admin = _context.Admins.Where(p => p.UserName == adminForLoginDto.UserName).FirstOrDefault();
                var change = await _authRepository.ChangePassAdmin(admin, adminForLoginDto.Password);
                return StatusCode(201);
            }
            return BadRequest(ModelState);

        }
        [HttpPost("adminlogin")]
        public async Task<ActionResult> Login([FromBody] AdminForLoginDto adminForLoginDto)
        {
            var admin = await _authRepository.LoginAdmin(adminForLoginDto.UserName, adminForLoginDto.Password);
            if (admin == null)
            {
                return Unauthorized();
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, admin.AdminId.ToString()),
                    new Claim(ClaimTypes.Name, admin.UserName)
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
