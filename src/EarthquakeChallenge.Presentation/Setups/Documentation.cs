using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EarthquakeChallenge.Setups
{
    public static class Documentation
    {
        public static IServiceCollection AddDocumentation(this IServiceCollection services)
        {
            services.AddOpenApiDocument((settings, provider) =>
            {
                settings.Title = "Earthquake Challenge API";
                settings.Description = "Powered by .Net Core 3.1 for Top.gg application.";
                settings.GenerateXmlObjects = true;
            });
            return services;
        }

        public static IApplicationBuilder UseDocumentation(this IApplicationBuilder builder, IConfiguration configuration)
        {
            builder
                .UseOpenApi()
                .UseReDoc((settings) =>
                {
                    settings.Path = configuration["Api:Routes:Documentation"];
                })
                .UseSwaggerUi3((settings) =>
                {
                    settings.Path = configuration["Api:Routes:Explorer"];
                    settings.OperationsSorter = "method";
                    settings.TagsSorter = "alpha";
                });

            return builder;
        }
    }
}
