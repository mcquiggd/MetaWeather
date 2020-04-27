﻿using Newtonsoft.Json;

namespace MetaWeather.Core.Interfaces
{
    public interface ILocation
    {
        [JsonProperty("title")]
        string Title { get; set; }

        [JsonProperty("location_type")]
        string LocationType { get; set; }

        [JsonProperty("woeid")]
        int Woeid { get; set; }

        [JsonProperty("latt_long")]
        string LattLong { get; set; }
    }
}