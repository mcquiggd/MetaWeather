using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using MetaWeather.Core.Entities;
using Newtonsoft.Json;

namespace MetaWeather.Tests.Common
{
    /// <summary>
    /// Follow a builder pattern to provide easily readable, known sets of test data.   e.g. var weatherBuilder = new
    /// TestWeatherResponseBuilder();  weatherBuilder.Default().WithBelfast().BuildHttpResponse();
    /// weatherBuilder.WithBelfast().WithBirmingham().BuildHttpResponse();
    /// weatherBuilder.WithStatusCode(HttpStatusCode.NotFound).BuildHttpResponse();
    /// </summary>
    public class TestLocationResponseBuilder
    {
        private LocationResponse _locationResponse;

        public LocationResponse Build() => _locationResponse;

        public async Task<HttpResponseMessage> BuildHttpResponse() =>
                                               await Task.FromResult(new HttpResponseMessage(_locationResponse.StatusCode)
                                                   {
                                                       Content =
                                                   new StringContent(JsonConvert.SerializeObject(_locationResponse.Locations))
                                                   })
                                                         .ConfigureAwait(false);

        public TestLocationResponseBuilder Default()
        {
            _locationResponse = new LocationResponse
            { StatusCode = HttpStatusCode.OK, Locations = new List<Location>() };

            return this;
        }

        public TestLocationResponseBuilder WithBelfast()
        {
            _locationResponse.Locations.Add(new Location { Title = "Belfast", WoeId = 44544, LocationType = "City" });

            return this;
        }

        public TestLocationResponseBuilder WithBirmingham()
        {
            _locationResponse.Locations
                .AddRange(new List<Location>
                {
                    new Location
                    { Title = "Birmingham", WoeId = 12723, LocationType = "City" },

                    new Location
                    { Title = "Birmingham", WoeId = 2364559, LocationType = "City" }
                });

            return this;
        }

        public TestLocationResponseBuilder WithStatusCode(HttpStatusCode httpStatusCode)
        {
            _locationResponse.StatusCode = httpStatusCode;

            return this;
        }
    }
}