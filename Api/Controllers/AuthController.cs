using Api.Services.Interfaces.UsuarioSer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Vm;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IConfiguration configuration, IUsuarioService usuarioService, ILogger<AuthController> logger)
        {
            _config = configuration;
            _logger = logger;
            _usuarioService = usuarioService;
        }
        /// <summary>
        /// Inicio de sesion con generación de token para accerder a la autorización
        /// </summary>
        /// <param name="vmAuth"></param>
        /// <returns>Token</returns>
        /// <response code="200"></response>
        /// 
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] VmAuth vmAuth)
        {
            try
            {
                //Consultar usuario y contraseña
                var Query = await _usuarioService.ListUsuarios(x => x.Nombre.Equals(vmAuth.Nombre) && x.Correo.Equals(vmAuth.Correo));
                if (Query.Any())
                {
                    var dato = Query.FirstOrDefault();
                    //Pasa a generar el Token
                    var token = CreateToken(vmAuth.Nombre);
                    dato.Token = token.Token;
                    await _usuarioService.ActualizarUsuario(dato);
                    return Ok(new { token = token.Token, exp = token.Expiration, idusuario = dato.IdUsuario });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al realizar {Login}");
            }
            return BadRequest();
        }

        #region Crear Token
        private (string Token, DateTime Expiration) CreateToken(string username)
        {
            var jwtKey = _config["Jwt:Key"];
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var expiration = DateTime.UtcNow.AddDays(1);
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return (Token: tokenString, Expiration: expiration);
        }
        #endregion

    }
}
