using System;

namespace EarthquakeChallenge
{
    public class Earthquake
    {
        public DateTime Time { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public decimal Magnitude { get; set; }
    }
}
