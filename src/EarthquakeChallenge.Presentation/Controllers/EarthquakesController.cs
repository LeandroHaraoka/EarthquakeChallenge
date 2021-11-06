using EarthquakeChallenge.Domain.Earthquakes;
using EarthquakeChallenge.Services.Handlers;
using EarthquakeChallenge.Services.Messages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EarthquakeChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EarthquakesController : ControllerBase
    {
        public EarthquakesController() { }

        [HttpGet]
        public async Task<IEnumerable<Earthquake>> Get([FromQuery] EarthquakeGetRequest request)
        {
            var earthquakes = new FindEarthquakesHandler();
            var result = await earthquakes.Find(request);
            return result;
        }
    }
}
