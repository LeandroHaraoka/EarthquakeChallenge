using EarthquakeChallenge.Domain.Entities;
using System;

namespace EarthquakeChallenge.Application.Clients.USGS
{
    public class EarthquakeResponse
    {
        public Feature[] Features { get; set; }
    }

    public class Feature
    {
        public Properties Properties { get; set; }
        public Geometry Geometry { get; set; }

        public Earthquake MapToEarthquake()
        {
            return new Earthquake(
                time: Properties.DateTime,
                longitude: Geometry.Coordinates[0],
                latitude: Geometry.Coordinates[1],
                magnitude: double.Parse(Properties.Mag));
        }
    }

    public class Properties
    {
        public string Mag { get; set; }
        public long Time { get; set; }
        public DateTime DateTime
        {
            get
            {
                var date = DateTime.UnixEpoch.AddMilliseconds(Time).ToUniversalTime();
                return date;
            }
        }
    }

        public class Geometry
    {
        public double[] Coordinates { get; set; }
    }
}
