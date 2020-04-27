using System.Linq;
using System.Net;

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
    public class LocationSpecs
    /*
    The application will communicate to the following REST API: https://www.metaweather.com/api/
    and retrieve the next 5 days of weather for Belfast.
    */
    {
        IMetaWeatherService    _metaWeatherService;
        MockHttpMessageHandler _mockHttpMessageHandler;

        [Scenario]
        [Example("NotBelfast")]
        [Example("NotBirmingham")]
        public void Api_Submit_InvalidLocationRequest_ReturnsNotFound(string cityName,
                                                                      ApiProxy apiProxy,
                                                                      ILocationRequest locationRequest,
                                                                      ILocationResponse locationResponse)
        {
            $"Given a cityName value of {cityName}".x(() =>
            {
                locationRequest = new LocationRequest { CityName = cityName };
            });

            "And an ApiProxy".x(() =>
            {
                apiProxy = new ApiProxy(_metaWeatherService);
            });


            "When the location request is submitted".x(async() =>
                locationResponse = await apiProxy.SubmitLocationRequest(locationRequest).ConfigureAwait(false));

            "Then the location response should return StatusCode 404, and Locations should be empty".x(() =>
            {
                using(new AssertionScope())
                {
                    locationResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
                    locationResponse.Locations.Should().BeNullOrEmpty();
                }
            });
        }

        [Scenario]
        [Example("Belfast", 1, 44544)]
        [Example("Birmingham", 2, 12723)]
        public void Api_Submit_ValidLocationRequest_ReturnsCorrectWoeid(string cityName,
                                                                        int expectedCount,
                                                                        int expectedWoeid,
                                                                        ApiProxy apiProxy,
                                                                        ILocationRequest locationRequest,
                                                                        ILocationResponse locationResponse)
        {
            $"Given a cityName value of {cityName}".x(() =>
            {
                locationRequest = new LocationRequest { CityName = cityName };
            });

            "And an ApiProxy".x(() =>
            {
                apiProxy = new ApiProxy(_metaWeatherService);
            });


            "When the location request is submitted".x(async() =>
                locationResponse = await apiProxy.SubmitLocationRequest(locationRequest).ConfigureAwait(false));

            $"Then the location response should have a StatusCode of Ok, CityName {cityName} and Woeid {expectedWoeid}".x(() =>
            {
                using(new AssertionScope())
                {
                    locationResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                    locationResponse.Locations.Should().HaveCount(expectedCount);
                    locationResponse.Locations.First().Woeid.Should().Be(expectedWoeid);
                }
            });
        }

        [Background]
        public void Background()
        {
            _mockHttpMessageHandler = new MockHttpMessageHandler();
            var builder = new TestLocationResponseBuilder();

            _mockHttpMessageHandler.When("https://www.metaweather.com/api/location/search")
                .WithQueryString("query", "Belfast")
                .Respond(() => builder.Default().WithBelfast().BuildHttpResponse());

            _mockHttpMessageHandler.When("https://www.metaweather.com/api/location/search")
                .WithQueryString("query", "Birmingham")
                .Respond(() => builder.Default().WithBirmingham().BuildHttpResponse());

            var settings = new RefitSettings { HttpMessageHandlerFactory = () => _mockHttpMessageHandler };

            _metaWeatherService = RestService.For<IMetaWeatherService>("https://www.metaweather.com", settings);
        }
    }
}