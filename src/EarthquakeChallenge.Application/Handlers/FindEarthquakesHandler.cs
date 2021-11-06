using EarthquakeChallenge.Application.Clients.USGS;
using EarthquakeChallenge.Application.Messages;
using EarthquakeChallenge.Domain.Entities;
using EarthquakeChallenge.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EarthquakeChallenge.Application.Handlers
{
    public interface IGetEarthquakesHandler
    {
        Task<IEnumerable<Earthquake>> Handle(EarthquakeGetRequest earthquakeGetRequest);
    }

    public class FindEarthquakesHandler : IGetEarthquakesHandler
    {
        private readonly IEarthquakesQuery _earthquakesQuery;

        public FindEarthquakesHandler(IEarthquakesQuery earthquakesQuery)
        {
            _earthquakesQuery = earthquakesQuery;
        }

        public async Task<IEnumerable<Earthquake>> Handle(EarthquakeGetRequest earthquakeGetRequest)
        {
            var requestCoordinate = new RadiansCoordinate(earthquakeGetRequest.Latitude.Value, earthquakeGetRequest.Longitude.Value);
            
            var queryResponse = await _earthquakesQuery.Find(earthquakeGetRequest.StartDate.Value, earthquakeGetRequest.EndDate.Value);

            if (queryResponse.Result is false) return default;

            return queryResponse.Earthquakes
                .Where(x =>
                    x.Time.Date >= earthquakeGetRequest.StartDate.Value.Date &&
                    x.Time.Date <= earthquakeGetRequest.EndDate.Value.Date &&
                    x.DistanceFromCoordinate(requestCoordinate) <= x.TravelDistance)
                .OrderByDescending(x => x.Time)
                .Take(10);
        }
    }
}
