using Models.Models;
using System.Linq.Expressions;

namespace Api.Data.Repositories.Interfaces.ITarea
{
    public interface ITareaRep
    {
        Task<IEnumerable<Tarea>> ListTareas (Expression<Func<Tarea, bool>> Function);
        Task<Tarea> GuardarTarea(Tarea tarea);
        Task<Tarea> SeleccionarID(int id);
        Task<Tarea> ActualizarTarea(Tarea tarea);
    }
}
