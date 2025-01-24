using Models.Models;
using Api.Data.Repositories.Interfaces.IUsuario;
using Api.Services.Interfaces.UsuarioSer;
using System.Linq.Expressions;

namespace Api.Services.Implementaciones.UsuarioImp
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRep _usuarioRep;
        private readonly ILogger _logger;

        public UsuarioService(IUsuarioRep usuarioRep, ILogger<UsuarioService> logger)
        {
            _logger = logger;
            _usuarioRep = usuarioRep;
        }

        public async Task<Usuario> ActualizarUsuario(Usuario usuario)
        {
            try
            {
                return await _usuarioRep.ActualizarUsuario(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(ActualizarUsuario)}");
            }
            return usuario;
        }

        public async Task<Usuario> GuardarUsuario(Usuario usuario)
        {
            try
            {
                return await _usuarioRep.GuardarUsuario(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(GuardarUsuario)}");
            }
            return usuario;
        }

        public async Task<IEnumerable<Usuario>> ListUsuarios(Expression<Func<Usuario, bool>> Function)
        {
            try
            {
                return await _usuarioRep.ListUsuarios(Function);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(ListUsuarios)}"); 
            }
            return default;
        }

        public async Task<Usuario> SeleccionarID(int id)
        {
            try
            {
                return await _usuarioRep.SeleccionarID(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(SeleccionarID)}");
            }
            return default;
        }
    }
}
