using System;
using System.Collections.Generic;
using System.Net;

using MetaWeather.Core.Entities;

using Newtonsoft.Json;

namespace MetaWeather.Core.Interfaces
{
    public interface IWeatherResponse
    {
        [JsonProperty("consolidated_weather")]
        List<Forecast> Forecasts { get; set; }

        HttpStatusCode StatusCode { get; set; }

        [JsonProperty("time")]
        DateTimeOffset Time { get; set; }

        [JsonProperty("timezone")]
        string TimeZone { get; set; }

        [JsonProperty("timezone_name")]
        string TimeZoneName { get; set; }

        [JsonProperty("title")]
        string Title { get; set; }

        [JsonProperty("woeid")]
        long WoeId { get; set; }
    }
}
