using EarthquakeChallenge.Domain.Extensions;
using EarthquakeChallenge.Domain.ValueObjects;
using System;

namespace EarthquakeChallenge.Domain.Entities
{
    public class Earthquake
    {
        public DateTime Time { get; private set; }

        public double Latitude { get; private set; }

        public double Longitude { get; private set; }

        public double Magnitude { get; private set; }

        public double TravelDistance { get => Magnitude * 100; }

        public Earthquake()
        {
        }

        public Earthquake(DateTime time, double latitude, double longitude, double magnitude)
        {
            Time = time;
            Latitude = latitude;
            Longitude = longitude;
            Magnitude = magnitude;
        }

        public double DistanceFromCoordinate(RadiansCoordinate coordinate) =>
           MathExtensions.GreatCircleDistance(new RadiansCoordinate(Latitude, Longitude), coordinate);
    }
}
