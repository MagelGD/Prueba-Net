using Models.Models;
using Api.Services.Interfaces.UsuarioSer;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioService usuarioService, ILogger<UsuarioController> logger)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Metodo para obtener lista de usuarios completos
        /// </summary>
        /// <returns>Devuelve una lista de usuarios</returns>
        // GET: api/<UsuarioController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> Get()
        {
            try
            {
                return Ok(await _usuarioService.ListUsuarios(x => x.Estado.Equals(true)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(Get)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Metodo para seleccionar un usuario especifico
        /// </summary>
        /// <param name="id">Id del usuario</param>
        /// <returns>Devuelve el modelo especifico del usuario</returns>
        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            try
            {
                return Ok(await _usuarioService.SeleccionarID(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(Get)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Metodo para guardar un usuario
        /// </summary>
        /// <param name="value">Modelo a guardar</param>
        /// <returns>Devuelve el modelo a guardar</returns>
        // POST api/<UsuarioController>
        [HttpPost("GuardarUsuario")]
        public async Task<ActionResult<Usuario>> GuardarUsuario([FromBody] Usuario value)
        {
            try
            {
                return Ok(await _usuarioService.GuardarUsuario(value));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(GuardarUsuario)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Metodo para actualizar un usuario
        /// </summary>
        /// <param name="value">Modelo a actualizar</param>
        /// <returns>Devuelve el modelo actualizado</returns>
        // POST api/<UsuarioController>
        [HttpPost("ActualizarUsuario")]
        public async Task<ActionResult<Usuario>> ActualizarUsuario([FromBody] Usuario value)
        {
            try
            {
                return Ok(await _usuarioService.ActualizarUsuario(value));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(ActualizarUsuario)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        //// PUT api/<UsuarioController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UsuarioController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
