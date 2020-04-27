using System;

using MetaWeather.Core.Interfaces;

namespace MetaWeather.Core.Entities
{
    public class Forecast : IForecast
    {
        public DateTimeOffset ApplicableDate { get; set; }

        public decimal TheTemp { get; set; }

        public string WeatherStateAbbr { get; set; }

        public string WeatherStateImageURL { get; set; }

        public string WeatherStateName { get; set; }
    }
}
