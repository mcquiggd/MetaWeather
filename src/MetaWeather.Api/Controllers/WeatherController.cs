using System;
using System.Threading.Tasks;

using MetaWeather.Api.Models;
using MetaWeather.Core.Entities;
using MetaWeather.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetaWeather.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WeatherController : ControllerBase
    {
        readonly ApiOptions _apiOptions;
        readonly IApiProxy _apiProxy;
        readonly ILogger<WeatherController> _logger;

        public WeatherController(ILogger<WeatherController> logger, ApiOptions apiOptions, IApiProxy apiProxy)
        {
            _logger = logger;
            _apiOptions =
                apiOptions ?? throw new ArgumentNullException(nameof(apiOptions));
            _apiProxy = apiProxy;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [HttpPost]
        public async Task<ActionResult> Post(WeatherRequest weatherRequest)
        {
            // Model state validation currently 'implicit' - would implement FluentValidation, to align
            // business rules
            // TODO: Add logic to validate and return correct StatusCode

            var results = await _apiProxy.SubmitWeatherRequest(weatherRequest).ConfigureAwait(false);

            return Ok(results);
        }
    }
}
