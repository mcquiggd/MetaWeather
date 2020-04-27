using System;

using Newtonsoft.Json;

namespace MetaWeather.Core.Interfaces
{
    public interface IForecast
    {
        [JsonProperty("applicable_date")]
        DateTimeOffset ApplicableDate { get; set; }

        [JsonProperty("the_temp")]
        decimal TheTemp { get; set; }

        [JsonProperty("weather_state_abbr")]
        string WeatherStateAbbr { get; set; }

        [JsonProperty("weather_state_name_image_url")]
        string WeatherStateImageURL { get; set; } //  => $"{_apiOptions.ApiUrl}/static/img/weather/{WeatherStateAbbr}.svg";

        [JsonProperty("weather_state_name")]
        string WeatherStateName { get; set; }
    }
}
