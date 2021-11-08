using AutoFixture;
using EarthquakeChallenge.Domain.Entities;
using System;
using System.Collections.Generic;

namespace EarthquakeChallenge.UnitTests.Application
{
    public static class EarthquakesFactory
    {
        public static IEnumerable<Earthquake> CreateMany(int count)
        {
            var fixture = new Fixture();
            var random = new Random();
            for (var i = 0; i < count; i++)
            {
                yield return new Earthquake
                    (
                        time: fixture.Create<DateTime>(),
                        latitude: RandomDoubleBetween(random, -90, 90),
                        longitude: RandomDoubleBetween(random, -180, 180),
                        magnitude: RandomDoubleBetween(random, 100, 105)
                    );
            }
        }

        private static double RandomDoubleBetween(Random random, double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }
    }
}
