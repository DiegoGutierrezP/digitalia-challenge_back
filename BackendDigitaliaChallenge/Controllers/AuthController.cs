using BackendDigitaliaChallenge.Context;
using BackendDigitaliaChallenge.Helpers;
using BackendDigitaliaChallenge.Models;
using BackendDigitaliaChallenge.Models.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackendDigitaliaChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext context;
        private IConfiguration _config;
        public AuthController(AppDbContext context, IConfiguration _config)
        {
            this.context = context;
            this._config = _config; 
        }

        [HttpPost]
        [Route("RegistrarUsuario")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUsuario([FromBody] RegistrarUsuarioRequest request)
        {
            try
            {

                var existsEmail = await context.usuario.Where(u => u.email == request.email).FirstOrDefaultAsync();

                if (existsEmail != null)
                {
                    return BadRequest(new
                    {
                        Res = false,
                        StatusCode = 500,
                        Message = "El email ya esta en uso",
                        Data = ""
                    });
                }

                string passEncriptado = EncriptacionHelper.Encriptar(request.password);

                await context.usuario.AddAsync(new Usuario()
                {
                    nombres = request.nombres,
                    apellidos = request.apellidos,
                    email = request.email,
                    password = passEncriptado,
                });

                await context.SaveChangesAsync();


                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = "Usuario registrado correctamente",
                    Data = ""
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Res = false,
                    StatusCode = 500,
                    Message = "Ocurrio un error al registrar al usuario",
                    Data = ""
                });
            } 
        }


        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {

                var user = await context.usuario.Where(u => u.email == request.email).FirstOrDefaultAsync();

                if (user == null)
                {
                    return BadRequest(new
                    {
                        Res = false,
                        StatusCode = 404,
                        Message = "El email no esta registrado",
                        Data = ""
                    });
                }

                string passDesencrip = EncriptacionHelper.Desencriptar(user.password);
                if(request.password != passDesencrip)
                {
                    return BadRequest(new
                    {
                        Res = false,
                        StatusCode = 500,
                        Message = "El password es incorrecto",
                        Data = ""
                    });
                }

                string token = GenerateToken(user);

                //string token = "";


                return Ok(new
                {
                    Res = true,
                    StatusCode = 200,
                    Message = "Login correcto",
                    Data = new {
                        user,
                        token
                    }
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Res = false,
                    StatusCode = 500,
                    Message = "Ocurrio un error al registrar al usuario",
                    Data = ""
                });
            }
        }


        private string GenerateToken(Usuario user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                //Expires = DateTime.UtcNow.AddMinutes(Constants.JWT_TOKEN_VALIDITY_MINS), // 5-10 
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
