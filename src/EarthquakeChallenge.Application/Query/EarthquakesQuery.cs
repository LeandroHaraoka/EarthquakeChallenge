using EarthquakeChallenge.Domain.Entities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EarthquakeChallenge.Application.Clients.USGS
{
    public interface IEarthquakesQuery
    {
        Task<(bool Result, IEnumerable<Earthquake> Earthquakes)> Find(DateTime startDate, DateTime endDate);
    }

    public sealed class EarthquakesQuery : IEarthquakesQuery
    {
        private readonly HttpClient _httpClient;
        private readonly USGSOptions _options;

        public EarthquakesQuery(HttpClient httpClient, IOptions<USGSOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<(bool, IEnumerable<Earthquake>)> Find(DateTime startDate, DateTime endDate)
        {
            try
            {
                var endpoint = string.Format(
                    _options.EarthquakeCatalogEndpoint,
                    startDate.ToString("yyyy-MM-dd"),
                    endDate.ToString("yyyy-MM-dd"));

                Uri.TryCreate($"{_options.BaseAddress}{endpoint}", UriKind.Absolute, out var requestUri);

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

                var response = await _httpClient.SendAsync(requestMessage);

                if (response.Content is null || !response.IsSuccessStatusCode) return (false, default);

                var earthquakes = await DeserializeEarthquakeResponse(response);

                return (true, earthquakes);
            }
            catch
            {
                return (false, default);
            }
        }

        private async Task<IEnumerable<Earthquake>> DeserializeEarthquakeResponse(HttpResponseMessage response)
        {
            var serializedContent = await response.Content.ReadAsStringAsync();
            var deserializedContent = JsonConvert.DeserializeObject<EarthquakeResponse>(serializedContent);
            return deserializedContent.Features.Select(x => x.MapToEarthquake());
        }
    }
}
