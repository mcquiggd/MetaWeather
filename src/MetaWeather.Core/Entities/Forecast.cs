using System;

using MetaWeather.Core.Interfaces;

namespace MetaWeather.Core.Entities
{
    public class Forecast : IForecast
    {
        public DateTimeOffset ApplicableDate { get; set; }

        public string DayOfWeek => ApplicableDate.DayOfWeek.ToString();

        public decimal TheTemp { get; set; }

        public string WeatherStateAbbr { get; set; }

        public string WeatherStateImageURL => $"https://www.metaweather.com/static/img/weather/{WeatherStateAbbr}.svg";

        public string WeatherStateName { get; set; }
    }
}
