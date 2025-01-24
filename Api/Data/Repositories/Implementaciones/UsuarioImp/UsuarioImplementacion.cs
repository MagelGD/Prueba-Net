using Models.Models;
using Api.Data.Repositories.Interfaces.IUsuario;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api.Data.Repositories.Implementaciones.UsuarioImp
{
    public class UsuarioImplementacion : IUsuarioRep
    {
        private readonly GestionTareasContext _Context;
        private readonly ILogger _logger;

        public UsuarioImplementacion(GestionTareasContext gestionTareasContext, ILogger<UsuarioImplementacion> logger)
        {
            _Context = gestionTareasContext;
            _logger = logger;
        }

        public async Task<Usuario> ActualizarUsuario(Usuario usuario)
        {
            Usuario Resultado = new();
            try
            {
                usuario.FechaModificacion = DateTime.Now;
                _Context.Update(usuario);
                if (await _Context.SaveChangesAsync() > 0)
                {
                    _Context.Entry(usuario).State = EntityState.Detached;
                    Resultado = usuario;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(ActualizarUsuario)}");
            }
            return Resultado;
        }

        public async Task<Usuario> GuardarUsuario(Usuario usuario)
        {
            Usuario Resultado = new();
            try
            {
                if(usuario.IdUsuario == 0)
                {
                    usuario.FechaCreacion = DateTime.Now;
                    _Context.Add(usuario);
                    if (await _Context.SaveChangesAsync() > 0)
                    {
                        _Context.Entry(usuario).State = EntityState.Detached;
                        Resultado = usuario;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(GuardarUsuario)}");
            }
            return Resultado;
        }

        public async Task<IEnumerable<Usuario>> ListUsuarios(Expression<Func<Usuario, bool>> Function)
        {
            IEnumerable<Usuario> Resultado = new List<Usuario>();
            try
            {
                var Qry = _Context.Usuario.AsQueryable();
                if (Function != null)
                {
                    Qry = Qry.Where(Function);
                }
                Resultado = await Qry.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el metodo {nameof(ListUsuarios)}");
            }
            return Resultado;
        }

        public async Task<Usuario> SeleccionarID(int id)
        {
            Usuario Resultado = new();
            try
            {
                var Qry = await _Context.Usuario.Where(x => x.IdUsuario.Equals(id)).SingleOrDefaultAsync();
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
