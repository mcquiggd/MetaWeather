using System.Collections.Generic;
using System.Threading.Tasks;
using MetaWeather.Core.Entities;
using Refit;

namespace MetaWeather.Application
{
    public interface IMetaWeatherService
    {
        [Get("/api/location/search?query={cityName}")]
        Task<ApiResponse<List<Location>>> GetLocationByCityName(string cityName);

        [Get("/api/location/{woeid}")]
        Task<ApiResponse<List<Forecast>>> GetWeatherByWoeId(int woeId);
    }
}