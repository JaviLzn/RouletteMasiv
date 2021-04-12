using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(setupAction: config =>
            {
                config.DefaultApiVersion = new ApiVersion(majorVersion: 1, minorVersion: 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(setupAction: options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }
    }
}
