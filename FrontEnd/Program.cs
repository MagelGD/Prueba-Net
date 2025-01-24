using Blazored.LocalStorage;
using Blazored.SessionStorage;
using FrontEnd;
using FrontEnd.Contenedores;
using FrontEnd.Helpers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


#region Configuracion Appsettings
// Determina el entorno actual (Desarrollo, Integración, Producción)
var environment = builder.HostEnvironment.Environment;

// Cargar el archivo de configuración apropiado basado en el entorno
if (environment == "Development")
{
    builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
}
else
{
    builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
}

#endregion
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();
builder.Services.TryAddScoped(typeof(IConexionApi<,>), typeof(ConexionApiService<,>));

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ContenedorUsuario>();

await builder.Build().RunAsync();
