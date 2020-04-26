using MetaWeather.Core.Interfaces;

namespace MetaWeather.Core.Entities
{
    public class LocationRequest : ILocationRequest
    {
        public string CityName { get; set; }
    }
}