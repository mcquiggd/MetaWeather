using MetaWeather.Core.Interfaces;

namespace MetaWeather.Core.Entities
{
    public class WeatherRequest : IWeatherRequest
    {
        public WeatherRequest() { }

        public int WoeId { get; set; }
    }
}
