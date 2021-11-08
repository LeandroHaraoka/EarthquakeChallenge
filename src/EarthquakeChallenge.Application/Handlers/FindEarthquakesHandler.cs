using EarthquakeChallenge.Application.Query;
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
            var requestCoordinate = new RadiansCoordinate(
                earthquakeGetRequest.Latitude.Value, earthquakeGetRequest.Longitude.Value);
            
            var (queryResult, earthquakes) = await _earthquakesQuery.Find(
                earthquakeGetRequest.StartDate.Value, earthquakeGetRequest.EndDate.Value);

            return queryResult is false 
                ? default
                : earthquakes
                    .Where(x =>
                        x.Time.Date >= earthquakeGetRequest.StartDate.Value.Date &&
                        x.Time.Date <= earthquakeGetRequest.EndDate.Value.Date &&
                        x.DistanceFromCoordinate(requestCoordinate) <= x.TravelDistance)
                    .OrderByDescending(x => x.Time)
                    .Take(10);
        }
    }
}
