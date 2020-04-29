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
    public class LocationController : ControllerBase
    {
        readonly ApiOptions _apiOptions;
        readonly IApiProxy _apiProxy;
        readonly ILogger<LocationController> _logger;

        public LocationController(ILogger<LocationController> logger, ApiOptions apiOptions, IApiProxy apiProxy)
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
        public async Task<ActionResult> Post(LocationRequest locationRequest)
        {
            // Model state validation currently 'implicit' - would implement FluentValidation, to align
            // business rules
            // TODO: Add logic to validate and return correct StatusCode

            var result = await _apiProxy.SubmitLocationRequest(locationRequest).ConfigureAwait(false);

            return Ok(result);
        }
    }
}
