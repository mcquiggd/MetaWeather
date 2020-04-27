using System.Threading.Tasks;

using MetaWeather.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MetaWeather.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        IApiProxy _apiProxy;

        public LocationController(IApiProxy apiProxy) => _apiProxy = apiProxy;

        public async Task<ActionResult<ILocationResponse>> Get([FromRoute] ILocationRequest locationRequest)
        {
            var results = await _apiProxy.SubmitLocationRequest(locationRequest);

            return results;
        }
    }
}
