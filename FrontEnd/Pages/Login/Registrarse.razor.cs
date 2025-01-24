using FrontEnd.Helpers;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using M = Models.Models;
namespace FrontEnd.Pages.Login
{
    public partial class Registrarse
    {
        private bool isLoading = true;
        private string Nombre { get; set; }
        private string Correo { get; set; }
        MudForm form;
        [Inject] private IConexionApi<M.Usuario, M.Usuario> _IConexionApi { get; set; }
        [Inject] private ILogger<Registrarse> _Logger { get; set; }
        private IEnumerable<M.Usuario> ListUsuarios { get; set; } = new List<M.Usuario>();
        protected override async Task OnInitializedAsync()
        {
            try
            {
                await Task.Delay(3000); // Simular carga de la PWA (3 segundos)
                isLoading = false; // Termina la carga
                ListUsuarios = await _IConexionApi.GetAll("Usuario");
            }
            catch (Exception ex)
            {
            }
        }
        private async Task Registrar()
        {
            try
            {
                await form.Validate();
                if(form.IsValid)
                {
                    if(!await ValidacionExistencia())
                    {
                        var usuario = new M.Usuario()
                        {
                            Nombre = Nombre,
                            Correo = Correo,
                            FechaCreacion = DateTime.Now
                        };
                        await _IConexionApi.Post("Usuario/GuardarUsuario", usuario);
                        _Isnackbar.Add($"Registro exitoso", Severity.Success);
                        Navigation.NavigateTo("/Login");
                    }
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, $"Error en realizar el registro");
            }
        }
        private async Task Volver()
        {
            try
            {
                Navigation.NavigateTo("/Login");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private async Task<bool> ValidacionExistencia()
        {
            try
            {
                if (ListUsuarios.Where(x => x.Nombre.Equals(Nombre.TrimStart().TrimEnd()) || x.Correo.Equals(Correo.TrimStart().TrimEnd())).Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
