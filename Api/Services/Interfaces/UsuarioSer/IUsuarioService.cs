using Models.Models;
using System.Linq.Expressions;

namespace Api.Services.Interfaces.UsuarioSer
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> ListUsuarios(Expression<Func<Usuario, bool>> Function);
        Task<Usuario> GuardarUsuario(Usuario usuario);
        Task<Usuario> SeleccionarID(int id);
        Task<Usuario> ActualizarUsuario(Usuario usuario);
    }
}
