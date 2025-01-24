using Blazored.LocalStorage;
using FrontEnd.Helpers;
using Microsoft.JSInterop;
using Models.Models;

namespace FrontEnd.Contenedores
{
    public class ContenedorUsuario
    {
        public Usuario usuario;

        private readonly ILocalStorageService _storageService;
        private readonly IConexionApi<Usuario, Usuario> _UsuarioApi;
        private readonly IJSRuntime _jsruntime;

        public ContenedorUsuario(ILocalStorageService localStorageService, IConexionApi<Usuario, Usuario> conexionApi, IJSRuntime jSRuntime)
        {
            _storageService = localStorageService;
            _jsruntime = jSRuntime;
            _UsuarioApi = conexionApi;
        }

        public event Action SetUsuario;

        public async Task ObtenerUsuario()
        {
            try
            {
                var id = await _storageService.GetItemAsync<int>("Identificador");
                if (id != 0)
                {
                    var dato = await _UsuarioApi.GetByItem($"Usuario/{id}");
                    if (dato != null)
                    {
                        usuario = dato;
                    }
                }
            }
            catch (Exception ex)
            {
                await _jsruntime.InvokeVoidAsync("console.log", ex);
            }
            ActionSetUsuario();
        }

        private void ActionSetUsuario() => SetUsuario?.Invoke();
    }

}
