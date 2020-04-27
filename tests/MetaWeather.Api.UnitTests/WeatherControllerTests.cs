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

            //Arrange
            _apiProxy = Substitute.For<IApiProxy>();
            _weatherResponseBuiler = new TestWeatherResponseBuilder();

            _weatherController = new WeatherController(logger, apiOptions, _apiProxy);
            _apiProxy.SubmitWeatherRequest(Arg.Any<IWeatherRequest>())
                .Returns(_weatherResponseBuiler.Default().WithBelfast().Build());

            //Act
            var weatherResponse = await _weatherController.Post(new WeatherRequest());

            using(new AssertionScope())
            {
                //weatherResponse.As<WeatherResponse>().StatusCode.Should().Be(HttpStatusCode.OK);
                weatherResponse.Should().BeOkObjectResult().ValueAs<IWeatherResponse>().Forecasts
                    .Should()
                    .HaveCount(expectedCount);


                //weatherResponse.Result.StatusCode.Should().Be(HttpStatusCode.OK);
                //weatherResponse.Result.Value.Forecasts.Should().HaveCount(expectedCount);
            }
        }
    }
}
