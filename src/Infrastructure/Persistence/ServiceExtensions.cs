using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Domain.Settings;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;

namespace Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionSettings>(configuration);
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
