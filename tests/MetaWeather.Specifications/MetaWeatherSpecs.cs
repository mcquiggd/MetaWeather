using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using Xbehave;

namespace MetaWeather.Specifications
{
    public class MetaWeatherSpecs
        /*
        The application will communicate to the following REST API: https://www.metaweather.com/api/
        and retrieve the next 5 days of weather for Belfast.
        */

    {
        [Scenario]
        [Example("Belfast", 44544)]
        public void Api_Submit_ValidLocationRequest_ReturnsCorrectWoeid(string cityName,
            int                                                                expectedWoeid,
            IApiProxy                                                          apiProxy,
            ILocationRequest                                                   locationRequest,
            ILocationResponse                                                  locationResponse)
        {
            $"Given a cityName value of {cityName}"
                .x(() =>
                {
                    locationRequest = new LocationRequest
                    {
                        CityName = cityName
                    };
                });

            "And an ApiProxy"
                .x(() => apiProxy = new ApiProxy());


            "When the location request is submitted"
                .x(async () => locationResponse =
                    await apiProxy.SubmitLocationRequest(locationRequest).ConfigureAwait(false));

            $"Then the location response should contain the correct CityName {cityName} and expected Woeid {expectedWoeid}"
                .x(() =>
                {
                    using (new AssertionScope())
                    {
                        locationResponse.Locations.Should().HaveCount(1);
                        locationResponse.Locations.First().Woeid.Should().Be(expectedWoeid);
                    }
                });
        }

        [Scenario]
        [Example("NotBelfast", 44544)]
        public void Api_Submit_InvalidLocationRequest_ReturnsError(string cityName,
            int                                                           expectedWoeid,
            IApiProxy                                                     apiProxy,
            ILocationRequest                                              locationRequest,
            ILocationResponse                                             locationResponse)
        {
            $"Given a cityName value of {cityName}"
                .x(() =>
                {
                    locationRequest = new LocationRequest
                    {
                        CityName = cityName
                    };
                });

            "And an ApiProxy"
                .x(() => apiProxy = new ApiProxy());


            "When the location request is submitted"
                .x(async () => locationResponse =
                    await apiProxy.SubmitLocationRequest(locationRequest).ConfigureAwait(false));

            "Then the location response should be empty"
                .x(() =>
                {
                    using (new AssertionScope())
                    {
                        locationResponse.Locations.Should().BeEmpty();
                    }
                });
        }
    }
}