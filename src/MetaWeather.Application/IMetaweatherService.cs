using System.Collections.Generic;
using System.Threading.Tasks;
using MetaWeather.Core.Entities;
using Refit;

namespace MetaWeather.Application
{
    public interface IMetaWeatherService
    {
        // Refit works with definitions of an interface, attributed with the HTTP calls and route parameters that 
        // will construct the actual call, when a Refit proxy is instantiated.
        [Get("/api/location/search?query={cityName}")]
        Task<ApiResponse<List<Location>>> GetLocationByCityName(string cityName);

        [Get("/api/location/{woeid}")]
        Task<ApiResponse<WeatherResponse>> GetWeatherByWoeId(int woeId);
    }
}