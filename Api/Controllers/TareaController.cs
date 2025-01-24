using Models.Models;
using Api.Services.Interfaces.TareaSer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly ITareaService _tareaService;
        private readonly ILogger<TareaController> _logger;

        public TareaController(ITareaService tareaService, ILogger<TareaController> logger)
        {
            _logger = logger;
            _tareaService = tareaService;
        }

        /// <summary>
        /// Metodo para obtener lista de tareas completas
        /// </summary>
        /// <returns>Retorna una lista</returns>
        // GET: api/<TareasController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarea>>> Get()
        {
            try
            {
                return Ok(await _tareaService.ListTareas(x => x.Estado.Equals(true)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(Get)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Metodo para obtener una lista de tareas filtrado por id de usuario
        /// </summary>
        /// <param name="id">id usuario</param>
        /// <returns>devuelve listado de tareas del usuario</returns>
        [HttpGet("PorUsuario/{id}")]
        public async Task<ActionResult<IEnumerable<Tarea>>> PorUsuario(int id)
        {
            try
            {
                return Ok(await _tareaService.ListTareas(x => x.Estado.Equals(true) && x.IdUsuario.Equals(id)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(PorUsuario)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Metodo para obtener un usuario especifico por id no es lista solo clase
        /// </summary>
        /// <param name="id">id de la tarea</param>
        /// <returns>devuelve solo el modelo no lista</returns>
        // GET api/<TareasController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarea>> Get(int id)
        {
            try
            {
                return Ok(await _tareaService.SeleccionarID(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(Get)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Metodo para gurdar tarea
        /// </summary>
        /// <param name="value">Modelo a guardar</param>
        /// <returns>devuele el modelo guardado</returns>
        // POST api/<TareasController>
        [HttpPost("GuardarTarea")]
        public async Task<ActionResult<Tarea>> GuardarTarea([FromBody] Tarea value)
        {
            try
            {
                return Ok(await _tareaService.GuardarTarea(value));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(GuardarTarea)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Metodo para actualizar tarea
        /// </summary>
        /// <param name="value"> modelo a actualizar</param>
        /// <returns>devuelve el modelo actualizado</returns>
        // POST api/<TareasController>
        [HttpPost("ActualizarTarea")]
        public async Task<ActionResult<Tarea>> ActualizarTarea([FromBody] Tarea value)
        {
            try
            {
                return Ok(await _tareaService.ActualizarTarea(value));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(ActualizarTarea)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        //// PUT api/<TareasController>/5
        //[HttpPut("{id}")]
        //public void Put(i[FromBody] string value)
        //{
        //}

        //// DELETE api/<TareasController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
