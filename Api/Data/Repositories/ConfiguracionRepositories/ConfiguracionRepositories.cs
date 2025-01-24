using Api.Data.Repositories.Implementaciones.TareaImp;
using Api.Data.Repositories.Implementaciones.UsuarioImp;
using Api.Data.Repositories.Interfaces.ITarea;
using Api.Data.Repositories.Interfaces.IUsuario;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Api.Data.Repositories.ConfiguracionRepositories
{
    public static class ConfiguracionRepositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.TryAddScoped<IUsuarioRep, UsuarioImplementacion>();
            services.TryAddScoped<ITareaRep, TareaImplementacion>();
            return services;
        }
    }
}
