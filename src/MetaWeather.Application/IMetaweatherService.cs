using System.Collections.Generic;
using System.Threading.Tasks;
using MetaWeather.Core.Entities;
using Refit;

namespace MetaWeather.Application
{
    public interface IMetaweatherService
    {
        [Get("/api/location/search?query={cityName}")]
        Task<ApiResponse<List<Location>>> GetLocationByCityName(string cityName);
    }
}