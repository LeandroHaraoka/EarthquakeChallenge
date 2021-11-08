using AutoFixture;
using EarthquakeChallenge.Application.Handlers;
using EarthquakeChallenge.Application.Messages;
using EarthquakeChallenge.Application.Query;
using EarthquakeChallenge.Domain.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EarthquakeChallenge.UnitTests.Application.Handlers
{
    public sealed class FindEarthquakesHandlerTests
    {

        private readonly Fixture _fixture;
        private readonly Mock<IEarthquakesQuery> _earthquakesQueryMock;
        private readonly FindEarthquakesHandler _findEarthquakesHandler;
        private readonly EarthquakeGetRequest _request;

        public FindEarthquakesHandlerTests()
        {
            _fixture = new Fixture();

            _earthquakesQueryMock = new Mock<IEarthquakesQuery>();

            _findEarthquakesHandler = new FindEarthquakesHandler(_earthquakesQueryMock.Object);

            _request = new EarthquakeGetRequest(
                latitude: 0, 
                longitude: 0, 
                startDate: _fixture.Create<DateTime>(), 
                endDate: _fixture.Create<DateTime>());
                
        }

        [Fact]
        public async Task Given_an_invalid_response__from_query_should_return_null()
        {
            // Arrange
            _earthquakesQueryMock
                .Setup(x => x.Find(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync((false, Enumerable.Empty<Earthquake>()));

            // Act
            var response = await _findEarthquakesHandler.Handle(_request);

            //Assert
            response.Should().BeNull();
            _earthquakesQueryMock
                .Verify(x => x.Find(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task Given_no_earthquakes_from_query_should_return_empty_collection()
        {
            // Arrange
            _earthquakesQueryMock
                .Setup(x => x.Find(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync((true, Enumerable.Empty<Earthquake>()));
            
            // Act
            var response = await _findEarthquakesHandler.Handle(_request);

            //Assert
            response.Should().BeEmpty();
            _earthquakesQueryMock
                .Verify(x => x.Find(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task Given_earthquakes_should_return_filtered_and_ordered_collection()
        {
            // Arrange
            var earthquakes = EarthquakesFactory.CreateMany(1000);
            var dates = earthquakes.Select(x => x.Time).ToArray();
            
            var request = new EarthquakeGetRequest(
                latitude: 0,
                longitude: 0,
                startDate: dates[2],
                endDate: dates[^2]);

            _earthquakesQueryMock
                .Setup(x => x.Find(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync((true, earthquakes));

            // Act
            var response = await _findEarthquakesHandler.Handle(request);

            //Assert
            response.Should().NotBeEmpty();
            response.Should().HaveCountLessThanOrEqualTo(10);
            response.Should().BeInDescendingOrder(x => x.Time);
            _earthquakesQueryMock
                .Verify(x => x.Find(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
        }
    }
}
