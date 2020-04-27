﻿using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using MetaWeather.Core.Entities;
using Newtonsoft.Json;

namespace MetaWeather.Tests.Common
{
    public class TestLocationResponseBuilder
    {
        private LocationResponse _locationResponse;

        public TestLocationResponseBuilder Default()
        {
            _locationResponse = new LocationResponse {StatusCode = HttpStatusCode.OK, Locations = new List<Location>()};

            return this;
        }

        public TestLocationResponseBuilder WithBelfast()
        {
            _locationResponse.Locations.Add(new Location
            {
                Title        = "Belfast",
                Woeid        = 44544,
                LocationType = "City"
            });

            return this;
        }

        public TestLocationResponseBuilder WithBirmingham()
        {
            _locationResponse.Locations.AddRange(new List<Location>
            {
                new Location
                {
                    Title        = "Birmingham",
                    Woeid        = 12723,
                    LocationType = "City"
                },

                new Location
                {
                    Title        = "Birmingham",
                    Woeid        = 2364559,
                    LocationType = "City"
                }
            });

            return this;
        }

        public TestLocationResponseBuilder WithStatusCode(HttpStatusCode httpStatusCode)
        {
            _locationResponse.StatusCode = httpStatusCode;

            return this;
        }

        public LocationResponse Build()
        {
            return _locationResponse;
        }

        public HttpResponseMessage BuildHttpResponse()
        {
            return new HttpResponseMessage(_locationResponse.StatusCode)
            {
                Content = new StringContent(JsonConvert.SerializeObject(_locationResponse.Locations))
            };
        }
    }
}