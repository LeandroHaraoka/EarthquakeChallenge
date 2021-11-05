using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace EarthquakeChallenge.Domain.Earthquakes.Catalog
{
    public interface IEarthquakeCatalogReader
    {
        Task<IEnumerable<Earthquake>> ReadFromCsvFile();
    }

    public sealed class EarthquakeCatalogReader : IEarthquakeCatalogReader
    {
        public async Task<IEnumerable<Earthquake>> ReadFromCsvFile()
        {
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = "Earthquakes\\Catalog\\all_month.csv";
            var fileFullPath = Path.Combine(basePath, filePath);

            var earthquakes = new List<Earthquake>();

            using (var reader = new StreamReader(fileFullPath))
            {
                var title  = await reader.ReadLineAsync();
                var line = await reader.ReadLineAsync();

                while (line != null)
                {
                    var parseResult = TryParseToEarthquake(line, out var newEarthquake);

                    if (parseResult) earthquakes.Add(newEarthquake);

                    line = await reader.ReadLineAsync();
                }
                
                return earthquakes;
            }

        }

        private bool TryParseToEarthquake(string catalogLine, out Earthquake earthquake)
        {
            earthquake = null;

            try
            {
                var splittedLine = catalogLine.Split(',');

                var time = DateTime.Parse(splittedLine[0]);
                var latitude = decimal.Parse(splittedLine[1]);
                var longitude = decimal.Parse(splittedLine[2]);
                var magnitude = decimal.Parse(splittedLine[4]);

                earthquake = new Earthquake(
                        time: time, latitude: latitude, longitude: longitude, magnitude: magnitude);

                return true;
            }
            catch 
            {
                return false;
            }
        }
    }

}
