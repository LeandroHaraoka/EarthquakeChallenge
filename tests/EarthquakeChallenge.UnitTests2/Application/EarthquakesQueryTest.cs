using AutoFixture;
using EarthquakeChallenge.Application.Clients.USGS;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace EarthquakeChallenge.UnitTests.Application
{
    public sealed class EarthquakesQueryTest
    {
        public EarthquakesQueryTest()
        {
            var x = 1;
        }

        [Fact]
        public async Task Given_a_valid_request_should_get_curves()
        {
            // Arrange
            var httpMessageHandlerMock = new Mock<HttpClient>();
            httpMessageHandlerMock
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(EarthquakeResponseMock.USGSClient)
                })
                .Verifiable();

            var clientOptions = Options.Create(
                new USGSOptions()
                {
                    BaseAddress = "https://www.domain.com",
                    EarthquakeCatalogEndpoint = "endpoint"
                });

            var query = new EarthquakesQuery(httpMessageHandlerMock.Object, clientOptions);

            var fixture = new Fixture();

            // Act
            var response = await query.Find(fixture.Create<DateTime>(), fixture.Create<DateTime>());

            //Assert
            response.Result.Should().BeTrue();
        }
    }
}
