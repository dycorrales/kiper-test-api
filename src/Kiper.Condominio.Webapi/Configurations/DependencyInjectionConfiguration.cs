using Kiper.Condominio.Infra.CrossCutting.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Kiper.Condominio.WebApi.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            DependencyInjectionResolver.RegisterServices(services);
        }
    }
}
