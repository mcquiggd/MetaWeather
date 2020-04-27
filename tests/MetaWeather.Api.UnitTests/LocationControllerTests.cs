using System.Threading.Tasks;

using FluentAssertions;
using FluentAssertions.AspNetCore.Mvc;
using FluentAssertions.Execution;

using MetaWeather.Api.Controllers;
using MetaWeather.Api.Models;
using MetaWeather.Core.Entities;
using MetaWeather.Core.Interfaces;
using MetaWeather.Tests.Common;
using Microsoft.Extensions.Logging;

using NSubstitute;

using Xunit;

namespace MetaWeather.Api.UnitTests
{
    public class LocationControllerTests

    {
        IApiProxy _apiProxy;
        LocationController _locationController;
        TestLocationResponseBuilder _locationResponseBuilder;

        public LocationControllerTests() { }

        [Theory]
        [AutoNSubstituteData]
        public async Task LocationController_GetWithValidWoeId_ReturnsValidLocationAsync(ILogger<LocationController> logger,
                                                                                         ApiOptions apiOptions,
                                                                                         IApiProxy apiProxy)
        {
            var expectedCount = 1;
            _apiProxy = apiProxy;

            // Arrange
            _apiProxy = Substitute.For<IApiProxy>();
            _locationResponseBuilder = new TestLocationResponseBuilder();

            _locationController = new LocationController(logger, apiOptions, _apiProxy);
            _apiProxy.SubmitLocationRequest(Arg.Any<ILocationRequest>())
                .Returns(_locationResponseBuilder.Default().WithBelfast().Build());

            // Act
            var locationResponse = await _locationController.Post(new LocationRequest());


            // Assert
            using(new AssertionScope())
            {
                //weatherResponse.As<WeatherResponse>().StatusCode.Should().Be(HttpStatusCode.OK);
                locationResponse.Should().BeOkObjectResult().ValueAs<ILocationResponse>().Locations
                    .Should()
                    .HaveCount(expectedCount);


                //weatherResponse.Result.StatusCode.Should().Be(HttpStatusCode.OK);
                //weatherResponse.Result.Value.Forecasts.Should().HaveCount(expectedCount);
            }
        }
    }
}
