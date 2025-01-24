using Models.Models;
using Api.Data.Repositories.Interfaces.ITarea;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api.Data.Repositories.Implementaciones.TareaImp
{
    public class TareaImplementacion : ITareaRep
    {
        private readonly GestionTareasContext _Context;
        private readonly ILogger _logger;

        public TareaImplementacion(GestionTareasContext gestionTareasContext, ILogger<TareaImplementacion> logger)
        {
            _logger = logger;
            _Context = gestionTareasContext;
        }

        public async Task<Tarea> ActualizarTarea(Tarea tarea)
        {
            Tarea Resultado = new();
            try
            {
                tarea.FechaModificacion = DateTime.Now;
                _Context.Update(tarea);
                if (await _Context.SaveChangesAsync() > 0)
                {
                    _Context.Entry(tarea).State = EntityState.Detached;
                    Resultado = tarea;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(ActualizarTarea)}");
            }
            return Resultado;
        }

        public async Task<Tarea> GuardarTarea(Tarea tarea)
        {
            Tarea Resultado = new();
            try
            {
                if (tarea.IdTarea == 0)
                {
                    tarea.FechaCreacion = DateTime.Now;
                    _Context.Add(tarea);
                    if (await _Context.SaveChangesAsync() > 0)
                    {
                        _Context.Entry(tarea).State = EntityState.Detached;
                        Resultado = tarea;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(GuardarTarea)}");
            }
            return Resultado;
        }

        public async Task<IEnumerable<Tarea>> ListTareas(Expression<Func<Tarea, bool>> Function)
        {
            IEnumerable<Tarea> Resultado = new List<Tarea>();
            try
            {
                var Qry = _Context.Tarea.AsQueryable();
                if (Function != default)
                {
                    Qry = Qry.Where(Function);
                }
                Resultado = await Qry.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(ListTareas)}");
            }
            return Resultado;
        }

        public async Task<Tarea> SeleccionarID(int id)
        {
            Tarea Resultado = new();
            try
            {
                var Qry = await _Context.Tarea.Where(x => x.IdTarea.Equals(id)).SingleOrDefaultAsync();
                return Qry ?? Resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(SeleccionarID)}");
            }
            return Resultado;
        }
    }
}
