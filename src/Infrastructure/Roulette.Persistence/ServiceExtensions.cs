using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Roulette.Domain.Settings;
using StackExchange.Redis;

namespace Roulette.Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<ConnectionSettings>>().Value;
                var configuration = ConfigurationOptions.Parse(settings.ConnectionString, true);
                configuration.ResolveDns = true;

                return ConnectionMultiplexer.Connect(configuration);
            });
        }
    }
}
