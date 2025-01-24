using FrontEnd.Helpers;
using Microsoft.AspNetCore.Components;
using Models.Vm;
using MudBlazor;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using static MudBlazor.CategoryTypes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FrontEnd.Pages.Login
{
    public partial class Login
    {
        private bool isLoading = true;
        private string Nombre { get; set; }
        private string Correo { get; set; }
        private bool IsError { get; set; } = true;
        MudForm form;

        [Inject] private IConexionApi<VmAuth, VmAuth> _IConexionApi { get; set; }
        [Inject] private ILogger<Login> _Logger { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await Task.Delay(3000); // Simular carga de la PWA (3 segundos)
                isLoading = false; // Termina la carga
            }
            catch (Exception ex)
            {

            }
        }
        private async Task Registrarse()
        {
            try
            {
                Navigation.NavigateTo("/Registrar");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private async Task Authenticacion()
        {
            try
            {
                IsError = true;
                await form.Validate();
                if (form.IsValid)
                {
                    var Auth = new VmAuth()
                    {
                        Nombre = Nombre,
                        Correo = Correo
                    };

                    var datos = await _IConexionApi.PostAnToken($"Auth/login", Auth);
                    if (datos != null)
                    {
                        VmToken token = JsonConvert.DeserializeObject<VmToken>(datos.ToString());
                        await localStorage.SetItemAsync("authToken", token.Token);
                        await localStorage.SetItemAsync("Identificador", token.idusuario);
                        _Isnackbar.Add($"Inicio sesión correcto", Severity.Success);
                        Navigation.NavigateTo("/");
                    }
                    else
                    {
                        _Isnackbar.Add($"Inicio Sesion Incorrecto", Severity.Warning);
                    }
                }

            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, $"Error en realizar el login");
                IsError = false;
            }
        }
    }
}
