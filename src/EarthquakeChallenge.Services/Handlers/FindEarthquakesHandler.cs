using EarthquakeChallenge.CrossCutting.ValueObjects;
using EarthquakeChallenge.Domain.Earthquakes;
using EarthquakeChallenge.Domain.Earthquakes.Catalog;
using EarthquakeChallenge.Services.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EarthquakeChallenge.Services.Handlers
{
    public class FindEarthquakesHandler
    {
        public async Task<IEnumerable<Earthquake>> Find(EarthquakeGetRequest earthquakeGetRequest)
        {
            var catalogReader = new EarthquakeCatalogReader();

            var requestCoordinate = new RadiansCoordinate(earthquakeGetRequest.Latitude.Value, earthquakeGetRequest.Longitude.Value);
            
            var earthquakes = await catalogReader.ReadFromCsvFile();

            earthquakes = earthquakes.
                Where(x =>
                    x.Time.Date >= earthquakeGetRequest.StartDate.Value.Date &&
                    x.Time.Date <= earthquakeGetRequest.EndDate.Value.Date &&
                    x.DistanceFromCoordinate(requestCoordinate) <= x.TravelDistance)
                .OrderByDescending(x => x.Time)
                .Take(10);

            return earthquakes;

        }
    }
}
