using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EarthquakeChallenge.Services.Clients
{
    public interface IUSGSClient
    {
        Task<IEnumerable<object>> GetEarthquakesSummary(object request);
    }

    public sealed class USGSClient
    {
        private readonly HttpClient _httpClient;

        public USGSClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<object>> GetEarthquakesSummary(object request)
        {
            var requestMessage = new HttpRequestMessage();

            var response = await _httpClient.SendAsync(requestMessage);

            return Enumerable.Empty<object>();
        }
    }
}
