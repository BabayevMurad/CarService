﻿using CarService.DataAccess.Abstract;
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
            if (await _authRepository.AdminExists(dto.Username))
            {
                return Conflict(new { message = "Username already exists" });
            }

            var adminToCreate = new Admin
            {
                Username = dto.Username,
            };

            await _authRepository.AdminRegister(adminToCreate, dto.Password);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("RegisterMechanic")]
        public async Task<ActionResult> RegisterMechanic([FromBody] MechanicRegisterDto dto)
        {
            if (await _authRepository.MechanicExists(dto.Username))
            {
                return Conflict(new { message = "Username already exists" });
            }

            var mechanicToCreate = new Mechanic
            {
                Username = dto.Username,
                Name = dto.Name,
                Surname = dto.Surname,
                WorkType = dto.WorkType,
                IsAccepted = false
            };

            await _authRepository.MexhanicRegister(mechanicToCreate, dto.Password);

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

            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value!);

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

            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value!);

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

        [HttpPost("LoginMechanic")]
        public async Task<ActionResult> MechanicLogin([FromBody] MechanicLoginDto dto)
        {
            var mechanic = await _authRepository.MechanicLogin(dto.Username, dto.Password);
            if (mechanic is null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,mechanic.Id.ToString()),
                    new Claim(ClaimTypes.Name,mechanic.Username)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new
            {
                token = tokenString,
                id = mechanic.Id,
                username = mechanic.Username,
                isAccepted = mechanic.IsAccepted,
                name = mechanic.Name,
                surname = mechanic.Surname,
                workType = mechanic.WorkType
            });
        }

        [HttpPost("AcceptedMechanicLogin")]
        public async Task<ActionResult> AcceptedMechanicLogin([FromBody] MechanicLoginDto dto)
        {
            var mechanic = await _authRepository.MechanicLogin(dto.Username, dto.Password);
            if (mechanic is null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,mechanic.Id.ToString()),
                    new Claim(ClaimTypes.Name,mechanic.Username)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new
            {
                token = tokenString,
                id = mechanic.Id,
                username = mechanic.Username
            });
        }

        [HttpPost("LogoutAdmin")]
        public async Task<ActionResult> AdminLogout([FromBody] AdminForLogin dto)
        {
            var admin = await _authRepository.AdminLogout(dto.Username, dto.Password);
            if (admin is null)
            {
                return Unauthorized();
            }

            return Ok();
        }
    }
}
