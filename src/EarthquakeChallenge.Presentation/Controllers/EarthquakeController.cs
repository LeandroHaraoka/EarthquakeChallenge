using EarthquakeChallenge.Messages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EarthquakeChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EarthquakeController : ControllerBase
    {
        public EarthquakeController() { }

        [HttpGet]
        public IEnumerable<Earthquake> Get([FromQuery] EarthquakeGetRequest earthquakeGetRequest)
        {
            return Enumerable.Empty<Earthquake>();
        }
    }
}
