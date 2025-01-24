using M = Models.Models;
using MudBlazor;
using Microsoft.AspNetCore.Components;
using FrontEnd.Helpers;
namespace FrontEnd.Pages.Maestras.Tarea
{
    public partial class AdminTarea
    {
        private string Busqueda = "";
        private bool _loading = true;
        private bool Estado { get; set; }
        private string infoFormat = "{first_item}-{last_item} de {all_items}";
        public IEnumerable<M.Tarea> ListTarea { get; set; } = new List<M.Tarea>();
        private bool pendiente { get; set; } = false;
        private bool Enprogreso { get; set; } = false;
        private bool Completada { get; set; } = false;

        private List<string> EstadosTareas { get; set; } = new List<string>();
        [Inject] IConexionApi<M.Tarea, M.Tarea> IconexionApiTarea { get; set; }

        [Inject] IDialogService DialogService { get; set; }
        string[] strings = new string[] { "Pendiente", "En Progreso", "Completada" };
        protected override async Task OnInitializedAsync()
        {
            try
            {
                await _ContenedorUsuario.ObtenerUsuario();
                await InicializarTabla();
                _loading = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task Filtrado(string value)
        {
            try
            {
                if (EstadosTareas.Where(x => x.Equals(value)).Any())
                {
                    EstadosTareas.Remove(value);
                }
                else
                {
                    EstadosTareas.Add(value);
                    ListTarea = ListTarea.Where(x => EstadosTareas.Contains(x.EstadoTarea));
                }
                if (!EstadosTareas.Any())
                {
                    await InicializarTabla();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private async Task InicializarTabla()
        {
            try
            {
                ListTarea = await IconexionApiTarea.GetAll($"Tarea/PorUsuario/{_ContenedorUsuario.usuario.IdUsuario}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool FilterFunc1(M.Tarea element) => FilterFunc(element, Busqueda);

        private bool FilterFunc(M.Tarea element, string Busqueda)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Busqueda))
                {
                    return true;
                }
                else
                {
                    if (element.Titulo.Contains(Busqueda, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    else if (element.Descripcion.Contains(Busqueda, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    else if (element.EstadoTarea.ToString().Contains(Busqueda, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
                return false;
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task CrearTarea()
        {
            try
            {
                var options = new DialogOptions { CloseOnEscapeKey = false, MaxWidth = MaxWidth.Medium, CloseButton = true, FullWidth = true };
                var parameters = new DialogParameters();
                parameters.Add("ListTarea", ListTarea);
                parameters.Add("EditTarea", null);
                var result = await DialogService.Show<MDCrearTarea>("Crear Tarea", parameters: parameters, options).Result;
                if (!result.Canceled)
                {
                    await InicializarTabla();
                    _loading = false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task EditarTarea(M.Tarea value)
        {
            try
            {
                var options = new DialogOptions { CloseOnEscapeKey = false, MaxWidth = MaxWidth.Medium, CloseButton = true, FullWidth = true };
                var parameters = new DialogParameters();
                parameters.Add("ListTarea", ListTarea);
                parameters.Add("EditTarea", value);
                var result = await DialogService.Show<MDCrearTarea>("Editar Tarea", parameters: parameters, options).Result;
                if (!result.Canceled)
                {
                    await InicializarTabla();
                    _loading = false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task CambiarEstado(M.Tarea value)
        {
            try
            {
                value.Estado = value.Estado ? false : true;
                value.FechaModificacion = DateTime.Now;
                var datos = await IconexionApiTarea.Post($"Tarea/ActualizarTarea", value);
                if (datos != null)
                {
                    _Isnackbar.Add("Se modifico el estado correctamente", Severity.Success);
                    await InicializarTabla();
                }
                else
                {
                    _Isnackbar.Add("No se pudo modificar el estado", Severity.Warning);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
