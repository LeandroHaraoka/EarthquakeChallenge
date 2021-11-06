using EarthquakeChallenge.Application.Clients.USGS;
using EarthquakeChallenge.Application.Handlers;
using EarthquakeChallenge.Application.Messages;
using EarthquakeChallenge.Presentation.Middlewares;
using EarthquakeChallenge.Setups;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;

namespace EarthquakeChallenge
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddFluentValidation();

            services
                .AddDocumentation()
                .AddSingleton<HttpClient>()
                .AddScoped<IValidator<EarthquakeGetRequest>, EarthquakeGetRequestValidator>()
                .AddScoped<IGetEarthquakesHandler, FindEarthquakesHandler>()
                .Configure<USGSOptions>(Configuration.GetSection("Clients:USGS"))
                .AddScoped<IEarthquakesQuery, EarthquakesQuery>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseDocumentation(Configuration);
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
