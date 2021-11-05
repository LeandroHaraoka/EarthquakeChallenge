using EarthquakeChallenge.Messages;
using EarthquakeChallenge.Setups;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                .AddHttpClient()
                .AddScoped<IValidator<EarthquakeGetRequest>, EarthquakeGetRequestValidator>()
                .AddTransient<IValidatorInterceptor, DefaultValidatorInterceptor>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseDocumentation(Configuration);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public class DefaultValidatorInterceptor : IValidatorInterceptor
    {
        public ValidationContext BeforeMvcValidation(ControllerContext controllerContext, ValidationContext validationContext)
        {
            return validationContext;
        }

        public ValidationResult AfterMvcValidation(ControllerContext controllerContext, ValidationContext validationContext,
            ValidationResult result)
        {
            if (!result.IsValid)
            {
                //var logEntry = new LogEntry()
                //    .SetMessage("Request validation failed")
                //    .SetSeverity(LogSeverity.Error)
                //    .AddTags("validate-request")
                //    .SetAdditionalData("Context", controllerContext.ActionDescriptor)
                //    .SetAdditionalData("Errors", result.Errors);
                //Logger.Log(logEntry);
            }

            return result;
        }
    }
}
