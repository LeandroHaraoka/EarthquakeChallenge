using System;

namespace EarthquakeChallenge.Domain.Earthquakes
{
    public class Earthquake
    {
        public DateTime Time { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public decimal Magnitude { get; set; }

        public Earthquake()
        {
        }

        public Earthquake(DateTime time, decimal latitude, decimal longitude, decimal magnitude)
        {
            Time = time;
            Latitude = latitude;
            Longitude = longitude;
            Magnitude = magnitude;
        }


    }
}
