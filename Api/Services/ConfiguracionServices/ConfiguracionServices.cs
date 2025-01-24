using Api.Services.Implementaciones.TareaImp;
using Api.Services.Implementaciones.UsuarioImp;
using Api.Services.Interfaces.TareaSer;
using Api.Services.Interfaces.UsuarioSer;

namespace Api.Services.ConfiguracionServices
{
    public static class ConfiguracionServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ITareaService, TareaService>();
            return services;
        }
    }
}
