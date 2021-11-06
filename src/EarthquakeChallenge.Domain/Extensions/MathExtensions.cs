using EarthquakeChallenge.Domain.ValueObjects;
using static System.Math;

namespace EarthquakeChallenge.Domain.Extensions
{
    public static class MathExtensions
    {
        private const double EarthRadius = 3959;

        public static double GreatCircleDistance(RadiansCoordinate point1, RadiansCoordinate point2) =>
            2 * EarthRadius * Asin(Sqrt(Haversine(point1, point2)));

        public static double Haversine(RadiansCoordinate point1, RadiansCoordinate point2)
        {
            var latitudeDifference = point2.Latitude - point1.Latitude;
            var longitudeDifference = point2.Longitude - point1.Longitude;

            return Haversine(latitudeDifference) +
                Cos(point1.Latitude) * Cos(point2.Latitude) * Haversine(longitudeDifference);
        }

        public static double Haversine(double angle) => (1 - Cos(angle)) / 2;
    }
}
