using System.Threading.Tasks;

using MetaWeather.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MetaWeather.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        IApiProxy _apiProxy;

        public WeatherController(IApiProxy apiProxy) => _apiProxy = apiProxy;

        public async Task<ActionResult<IWeatherResponse>> Post(IWeatherRequest weatherRequest)
        {
            var results = await _apiProxy.SubmitWeatherRequest(weatherRequest);

            return results;
        }
    }
}
