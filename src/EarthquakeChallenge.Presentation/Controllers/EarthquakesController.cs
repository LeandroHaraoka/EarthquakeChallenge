using EarthquakeChallenge.Domain.Earthquakes;
using EarthquakeChallenge.Domain.Earthquakes.Catalog;
using EarthquakeChallenge.Messages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EarthquakeChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EarthquakesController : ControllerBase
    {
        public EarthquakesController() { }

        [HttpGet]
        public async Task<IEnumerable<Earthquake>> Get([FromQuery] EarthquakeGetRequest earthquakeGetRequest)
        {
            var x = await new EarthquakeCatalogReader().ReadFromCsvFile();
            return Enumerable.Empty<Earthquake>();
        }
    }
}
