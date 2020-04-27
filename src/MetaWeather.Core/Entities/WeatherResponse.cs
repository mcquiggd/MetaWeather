using System;
using System.Collections.Generic;
using System.Net;
using MetaWeather.Core.Interfaces;

namespace MetaWeather.Core.Entities
{
    public class WeatherResponse : IWeatherResponse
    {
        public List<Forecast> Forecasts { get; set; } = new List<Forecast>();

        public HttpStatusCode StatusCode { get; set; }

        public DateTimeOffset Time { get; set; }

        public string TimeZone { get; set; }

        public string TimeZoneName { get; set; }

        public string Title { get; set; }

        public long WoeId { get; set; }
    }
}