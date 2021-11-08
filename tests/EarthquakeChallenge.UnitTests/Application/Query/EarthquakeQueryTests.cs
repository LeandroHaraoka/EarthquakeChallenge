using AutoFixture;
using EarthquakeChallenge.Application.Query;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EarthquakeChallenge.UnitTests.Application.Query
{
    public sealed class EarthquakesQueryTest
    {
        private readonly IOptions<USGSOptions> _clientOptions;
        private readonly Fixture _fixture;
        public EarthquakesQueryTest()
        {
            _fixture = new Fixture();
            _clientOptions = Options.Create(
                new USGSOptions()
                {
                    BaseAddress = "https://www.domain.com",
                    EarthquakeCatalogEndpoint = "endpoint"
                });
        }

        [Fact]
        public async Task Given_a_valid_usgs_response_should_return_earthquakes()
        {
            // Arrange
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(EarthquakeResponseMock.USGSClient)
                })
                .Verifiable();

            var query = new EarthquakesQuery(new HttpClient(httpMessageHandlerMock.Object), _clientOptions);

            // Act
            var response = await query.Find(_fixture.Create<DateTime>(), _fixture.Create<DateTime>());

            //Assert
            response.Result.Should().BeTrue();
            response.Earthquakes.Should().NotBeEmpty();
            response.Earthquakes.Should().NotContainNulls();
        }

        [Theory]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.Unauthorized)]
        public async Task Given_an_invalid_usgs_response_should_return_false_result(HttpStatusCode statusCode)
        {
            // Arrange
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = statusCode
                })
                .Verifiable();

            var query = new EarthquakesQuery(new HttpClient(httpMessageHandlerMock.Object), _clientOptions);

            // Act
            var response = await query.Find(_fixture.Create<DateTime>(), _fixture.Create<DateTime>());

            //Assert
            response.Result.Should().BeFalse();
            response.Earthquakes.Should().BeNull();
        }
    }
}
