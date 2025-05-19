using CarService.DataAccess.Abstract;
using CarService.Entities;
using CarService.Entities.Dto_s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        [HttpPost("RegisterUser")]
        public async Task<ActionResult> Register([FromBody] UserForRegisterDto dto)
        {
            if (await _authRepository.UserExists(dto.Username))
            {
                ModelState.AddModelError("Username", "Username already exist");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToCreate = new User
            {
                Username = dto.Username,
            };

            await _authRepository.Register(userToCreate, dto.Password);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("RegisterAdmin")]
        public async Task<ActionResult> RegisterAdmin([FromBody] AdminForRegister dto)
        {
            if (await _authRepository.UserExists(dto.Username))
            {
                ModelState.AddModelError("Username", "Username already exist");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var adminToCreate = new Admin
            {
                Username = dto.Username,
            };

            await _authRepository.AdminRegister(adminToCreate, dto.Password);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult> Login([FromBody] UserForLoginDto dto)
        {
            var user = await _authRepository.Login(dto.Username, dto.Password);
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
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.Username)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new
            {
                token = tokenString,
                id = user.Id,
                username = user.Username
            });
        }

        [HttpPost("LoginAdmin")]
        public async Task<ActionResult> AdminLogin([FromBody] AdminForLogin dto)
        {
            var admin = await _authRepository.AdminLogin(dto.Username, dto.Password);
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
                    new Claim(ClaimTypes.NameIdentifier,admin.Id.ToString()),
                    new Claim(ClaimTypes.Name,admin.Username)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new
            {
                token = tokenString,
                id = admin.Id,
                username = admin.Username
            });
        }
    }
}
