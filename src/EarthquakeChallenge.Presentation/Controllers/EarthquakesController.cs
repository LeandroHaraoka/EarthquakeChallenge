using EarthquakeChallenge.Domain.Entities;
using EarthquakeChallenge.Application.Handlers;
using EarthquakeChallenge.Application.Messages;
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
        private readonly IGetEarthquakesHandler _getEarthquakesHandler;

        public EarthquakesController(IGetEarthquakesHandler getEarthquakesHandler)
        {
            _getEarthquakesHandler = getEarthquakesHandler;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Earthquake>>> Get([FromQuery] EarthquakeGetRequest request)
        {
            var result = await _getEarthquakesHandler.Handle(request);

            if (result is null) return UnprocessableEntity("Failed while retrieving information for thirdy-party API.");

            if (result.Any() is false) return NotFound("No earthquakes were found for these parameters.");

            return Ok(result);
        }
    }
}
