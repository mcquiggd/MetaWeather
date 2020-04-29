using System.Net;
using System.Net.Http;

using FluentAssertions;
using FluentAssertions.Execution;
using MetaWeather.Application;
using MetaWeather.Core.Entities;
using MetaWeather.Core.Interfaces;
using MetaWeather.Tests.Common;
using Refit;
using RichardSzalay.MockHttp;
using Xbehave;

namespace MetaWeather.Specifications
{
    /// <summary>
    /// An initial set of Specifications, to enable investigation of interfaces / methods / properties that will need to
    /// be implemented to meet requirements. These are are examples
    /// </summary>
    public class WeatherSpecs
    {
        IMetaWeatherService    _metaWeatherService;
        MockHttpMessageHandler _mockHttpMessageHandler;

        [Scenario]
        [Example(12345)]
        [Example(00000)]
        public void Api_Submit_InvalidWeatherRequest_ReturnsNotFound(int woeId,
                                                                     ApiProxy apiProxy,
                                                                     IWeatherRequest weatherRequest,
                                                                     IWeatherResponse weatherResponse)
        {
            $"Given a woeId value of {woeId}".x(() =>
            {
                weatherRequest = new WeatherRequest { WoeId = woeId };
            });

            "And an ApiProxy".x(() =>
            {
                apiProxy = new ApiProxy(_metaWeatherService);
            });

            "When the weather request is submitted".x(async() =>
                weatherResponse = await apiProxy.SubmitWeatherRequest(weatherRequest).ConfigureAwait(false));

            "Then the weather response should return StatusCode 404, and Forecasts should be empty".x(() =>
            {
                using(new AssertionScope())
                {
                    weatherResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
                    weatherResponse.Forecasts.Should().BeNullOrEmpty();
                }
            });
        }

        [Scenario]
        [Example(44544, 5)]
        [Example(12723, 5)]
        public void Api_Submit_ValidWeatherRequest_ReturnsForecasts(int woeId,
                                                                    int expectedCount,
                                                                    ApiProxy apiProxy,
                                                                    IWeatherRequest weatherRequest,
                                                                    IWeatherResponse weatherResponse)
        {
            $"Given a woeId value of {woeId}".x(() =>
            {
                weatherRequest = new WeatherRequest { WoeId = woeId };
            });

            "And an ApiProxy".x(() =>
            {
                apiProxy = new ApiProxy(_metaWeatherService);
            });

            "When the weather request is submitted".x(async() =>
                weatherResponse = await apiProxy.SubmitWeatherRequest(weatherRequest).ConfigureAwait(false));

            $"Then the weather response should return StatusCode HttpStatusCode.OK), and contain {expectedCount} Forecasts".x(() =>
            {
                using(new AssertionScope())
                {
                    weatherResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                    weatherResponse.Forecasts.Should().HaveCount(expectedCount);
                }
            });
        }

        [Background]
        public void Background()
        {
            _mockHttpMessageHandler = new MockHttpMessageHandler();
            var locationBuilder = new TestLocationResponseBuilder();
            var weatherBuilder = new TestWeatherResponseBuilder();

            // TODO: Extract strings to constants

            _mockHttpMessageHandler.When("https://www.metaweather.com/api/location/search")
                .WithQueryString("query", "Belfast")
                .Respond(() => locationBuilder.Default().WithBelfast().BuildHttpResponse());

            _mockHttpMessageHandler.When("https://www.metaweather.com/api/location/search")
                .WithQueryString("query", "Birmingham")
                .Respond(() => locationBuilder.Default().WithBirmingham().BuildHttpResponse());

            _mockHttpMessageHandler.When("https://www.metaweather.com/api/location/44544")
                .Respond(() => weatherBuilder.Default().WithBelfast().BuildHttpResponse());

            _mockHttpMessageHandler.When("https://www.metaweather.com/api/location/12723")
                .Respond(() => weatherBuilder.Default().WithBirmingham().BuildHttpResponse());

            _mockHttpMessageHandler.Fallback
                .Respond(req =>
                new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    ReasonPhrase =
                $"No matching mock handler found for \"{req.Method.ToString().ToUpperInvariant()} {req.RequestUri.AbsoluteUri}\""
                });

            var settings = new RefitSettings { HttpMessageHandlerFactory = () => _mockHttpMessageHandler };

            _metaWeatherService = RestService.For<IMetaWeatherService>("https://www.metaweather.com", settings);
        }
    }
}