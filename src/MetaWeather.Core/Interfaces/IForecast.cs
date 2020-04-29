using System;

using Newtonsoft.Json;

namespace MetaWeather.Core.Interfaces
{
    // Note that it is possible to include attributes on interfaces, that are then subsequently
    // applied to implementing classes. This is useful when following a TDD / BDD approach, and working
    // with external data sources.
    public interface IForecast
    {
        [JsonProperty("applicable_date")]
        DateTimeOffset ApplicableDate { get; set; }

        [JsonProperty("the_temp")]
        decimal TheTemp { get; set; }

        [JsonProperty("weather_state_abbr")]
        string WeatherStateAbbr { get; set; }

        [JsonProperty("weather_state_name_image_url")]
        string WeatherStateImageURL { get; }

        [JsonProperty("weather_state_name")]
        string WeatherStateName { get; set; }
    }
}
