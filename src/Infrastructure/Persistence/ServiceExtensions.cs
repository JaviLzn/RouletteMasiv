using Application.Interfaces;
using Domain.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence.Repositories;
using StackExchange.Redis;

namespace Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionSettings>(config: configuration);
            services.AddSingleton(implementationFactory: sp =>
            {
                var settings = sp.GetRequiredService<IOptions<ConnectionSettings>>().Value;
                var configuration = ConfigurationOptions.Parse(configuration: settings.ConnectionString, ignoreUnknown: true);
                configuration.ResolveDns = true;

                return ConnectionMultiplexer.Connect(configuration: configuration);
            });
            services.AddTransient<IRouletteRepository, RouletteRepository>();
        }
    }
}
