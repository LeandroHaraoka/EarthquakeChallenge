using EarthquakeChallenge.Services.Messages;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace EarthquakeChallenge.Services
{
    public static class Setup
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IValidator<EarthquakeGetRequest>, EarthquakeGetRequestValidator>()
                .AddTransient<IValidatorInterceptor, DefaultValidatorInterceptor>();
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
