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
    /// <summary>
    /// These are examples, rather than an attempt at full test coverage.
    /// </summary>
    public class WeatherControllerTests

    {
        IApiProxy _apiProxy;
        WeatherController _weatherController;
        TestWeatherResponseBuilder _weatherResponseBuiler;

        public WeatherControllerTests() { }

        [Theory]
        [AutoNSubstituteData]
        public async Task WeatherController_GetWithValidWoeId_ReturnsValidWeatherResponse(ILogger<WeatherController> logger,
                                                                                          ApiOptions apiOptions,
                                                                                          IApiProxy apiProxy)
        {
            var expectedCount = 5;
            _apiProxy = apiProxy;

            // Arrange
            _apiProxy = Substitute.For<IApiProxy>();
            _weatherResponseBuiler = new TestWeatherResponseBuilder();

            _weatherController = new WeatherController(logger, apiOptions, _apiProxy);
            _apiProxy.SubmitWeatherRequest(Arg.Any<IWeatherRequest>())
                .Returns(_weatherResponseBuiler.Default().WithBelfast().Build());

            // Act
            var weatherResponse = await _weatherController.Post(new WeatherRequest());

            // Assert
            using(new AssertionScope())
            {
                weatherResponse.Should().BeOkObjectResult().ValueAs<IWeatherResponse>().Forecasts
                    .Should()
                    .HaveCount(expectedCount);
            }
        }
    }
}
