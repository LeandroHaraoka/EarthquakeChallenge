using FluentValidation;
using System;

namespace EarthquakeChallenge.Messages
{
    public class EarthquakeGetRequest
    {
        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }

    public sealed class EarthquakeGetRequestValidator : AbstractValidator<EarthquakeGetRequest>
    {
        public EarthquakeGetRequestValidator()
        {
            RuleFor(x => x.Latitude)
                .NotNull()
                .Must(x => x >= -90 && x <= 90)
                .WithMessage($"{nameof(EarthquakeGetRequest.Latitude)} value must be between -90 and 90 degrees.");
            RuleFor(x => x.Longitude)
                .NotNull()
                .Must(x => x >= -180 && x <= 180)
                .WithMessage($"{nameof(EarthquakeGetRequest.Longitude)} value must be between -180 and 180 degrees.");
            RuleFor(x => x.StartDate)
                .NotNull()
                .WithMessage($"{nameof(EarthquakeGetRequest.StartDate)} must not be null.");
            RuleFor(x => x.EndDate)
                .NotNull()
                .Must(x => x <= DateTime.UtcNow)
                .WithMessage($"{nameof(EarthquakeGetRequest.EndDate)} must be a value earlier than UTC.Now.");
            RuleFor(x => x)
                .Must(x => x.StartDate <= x.EndDate)
                .When(x => x.StartDate != null && x.EndDate!= null)
                .WithMessage($"{nameof(EarthquakeGetRequest.StartDate)} must be earlier or same as {nameof(EarthquakeGetRequest.EndDate)}.");
        }
    }
}
