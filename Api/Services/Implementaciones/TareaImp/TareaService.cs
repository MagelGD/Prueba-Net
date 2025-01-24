using Models.Models;
using Api.Data.Repositories.Interfaces.ITarea;
using Api.Services.Interfaces.TareaSer;
using System.Linq.Expressions;

namespace Api.Services.Implementaciones.TareaImp
{
    public class TareaService : ITareaService
    {
        private readonly ITareaRep _tareaRep;
        private readonly ILogger _logger;

        public TareaService(ITareaRep tareaRep, ILogger<TareaService> logger)
        {
            _logger = logger;
            _tareaRep = tareaRep;
        }

        public async Task<Tarea> ActualizarTarea(Tarea tarea)
        {
            try
            {
                return await _tareaRep.ActualizarTarea(tarea);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(ActualizarTarea)}");
            }
            return tarea;
        }

        public async Task<Tarea> GuardarTarea(Tarea tarea)
        {
            try
            {
                return await _tareaRep.GuardarTarea(tarea);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(GuardarTarea)}"); 
            }
            return tarea;
        }

        public async Task<IEnumerable<Tarea>> ListTareas(Expression<Func<Tarea, bool>> Function)
        {
            try
            {
                return await _tareaRep.ListTareas(Function);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(ListTareas)}");
            }
            return default;
        }

        public async Task<Tarea> SeleccionarID(int id)
        {
            try
            {
                return await _tareaRep.SeleccionarID(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(SeleccionarID)}");
            }
            return default;
        }
    }
}
