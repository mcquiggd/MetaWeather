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
        /*
         * Walk through the external api requirements to retrieve the valid woeid
        */

        [Example("Belfast", 44544)]
        public void Api_Submit_ValidLocationRequest_ReturnsCorrectWoeid(string cityName,
                                                                        int expectedWoeid,
                                                                        IApiProxy apiProxy,
                                                                        ILocationRequest locationRequest,
                                                                        ILocationResponse locationResponse)
        {
            $"Given a cityName value of {cityName}"
                            .x(() =>
            {
                locationRequest.CityName = cityName;
            });


            "When the location request is submitted"
                .x(async() => locationResponse =
                await apiProxy.SubmitLocationRequest(locationRequest).ConfigureAwait(false));

            "Then the location response should contain the correct woeid"
                .x(() =>
            {
                using(new AssertionScope())
                {
                    locationResponse.Locations.Should().HaveCount(1);
                    locationResponse.Locations.First().Woeid.Should().Be(expectedWoeid);
                }
            });
        }
    }
}
