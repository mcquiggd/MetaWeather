using System.Threading.Tasks;
using MetaWeather.Core.Entities;

namespace MetaWeather.Core.Interfaces
{
    public interface IApiProxy
    {
        Task<LocationResponse> SubmitLocationRequest(ILocationRequest locationRequest);

        Task<WeatherResponse> SubmitWeatherRequest(IWeatherRequest weatherRequest);
    }
}