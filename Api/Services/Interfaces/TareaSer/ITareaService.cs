using Models.Models;
using System.Linq.Expressions;

namespace Api.Services.Interfaces.TareaSer
{
    public interface ITareaService
    {
        Task<IEnumerable<Tarea>> ListTareas(Expression<Func<Tarea, bool>> Function);
        Task<Tarea> GuardarTarea(Tarea tarea);
        Task<Tarea> SeleccionarID(int id);
        Task<Tarea> ActualizarTarea(Tarea tarea);
    }
}
