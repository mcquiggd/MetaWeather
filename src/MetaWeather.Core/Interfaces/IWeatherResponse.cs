using System;
using System.Collections.Generic;
using System.Net;

using MetaWeather.Core.Entities;

using Newtonsoft.Json;

namespace MetaWeather.Core.Interfaces
{
    public interface IWeatherResponse
    {
        [JsonProperty("consolidated_weather", Order = 999)]
        List<Forecast> Forecasts { get; set; }

        HttpStatusCode StatusCode { get; set; }

        [JsonProperty("time", Order = 5)]
        DateTimeOffset Time { get; set; }

        [JsonProperty("timezone", Order = 4)]
        string TimeZone { get; set; }

        [JsonProperty("timezone_name", Order = 3)]
        string TimeZoneName { get; set; }

        [JsonProperty("title", Order = 2)]
        string Title { get; set; }

        [JsonProperty("woeid", Order = 1)]
        long WoeId { get; set; }
    }
}
