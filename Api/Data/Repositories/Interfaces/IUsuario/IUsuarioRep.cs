using Models.Models;
using System.Linq.Expressions;

namespace Api.Data.Repositories.Interfaces.IUsuario
{
    public interface IUsuarioRep
    {
        Task<IEnumerable<Usuario>> ListUsuarios(Expression<Func<Usuario, bool>> Function);
        Task<Usuario> GuardarUsuario(Usuario usuario);
        Task<Usuario> SeleccionarID(int id);
        Task<Usuario> ActualizarUsuario(Usuario usuario);
    }
}
