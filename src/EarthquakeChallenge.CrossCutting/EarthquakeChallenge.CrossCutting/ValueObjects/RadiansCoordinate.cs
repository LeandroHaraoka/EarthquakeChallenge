using System;

namespace EarthquakeChallenge.CrossCutting.ValueObjects
{
    public readonly struct RadiansCoordinate
    {
        public RadiansCoordinate(double latitude, double longitude)
        {
            Latitude = latitude * Math.PI / 180;
            Longitude = longitude * Math.PI / 180;
        }

        public readonly double Latitude { get; }
        public readonly double Longitude { get; }
    }
}
