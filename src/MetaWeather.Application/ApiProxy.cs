using MetaWeather.Core.Entities;
using MetaWeather.Core.Interfaces;

using Refit;

using System;
using System.Net;
using System.Threading.Tasks;

namespace MetaWeather.Application
{
    public class ApiProxy : IApiProxy
    {
        readonly IMetaWeatherService _metaWeatherService;

        public ApiProxy(IMetaWeatherService metaWeatherService) { _metaWeatherService = metaWeatherService; }

        public async Task<LocationResponse> SubmitLocationRequest(ILocationRequest locationRequest)
        {
            try
            {
                using(var apiResponse = await _metaWeatherService.GetLocationByCityName(locationRequest.CityName)
                    .ConfigureAwait(false))
                {
                    if(!apiResponse.IsSuccessStatusCode)
                    {
                        return new LocationResponse { StatusCode = apiResponse.StatusCode, Locations = null };
                    }

                    if(apiResponse.Content.Count == 0)
                    {
                        return new LocationResponse { StatusCode = HttpStatusCode.NotFound, Locations = null };
                    }

                    return new LocationResponse { StatusCode = HttpStatusCode.OK, Locations = apiResponse.Content };
                }
            } catch(Exception ex) when ((ex is ApiException) || (ex is WebException))
            {
                //Here we would Log exception
                return new LocationResponse { StatusCode = HttpStatusCode.InternalServerError, Locations = null };
            }
        }

        public async Task<WeatherResponse> SubmitWeatherRequest(IWeatherRequest weatherRequest)
        {
            try
            {
                using(var apiResponse = await _metaWeatherService.GetWeatherByWoeId(weatherRequest.WoeId)
                    .ConfigureAwait(false))
                {
                    if(!apiResponse.IsSuccessStatusCode)
                    {
                        return new WeatherResponse { StatusCode = apiResponse.StatusCode, Forecasts = null };
                    }

                    var forecasts = apiResponse.Content.Forecasts;

                    if(forecasts.Count == 0)
                    {
                        return new WeatherResponse { StatusCode = HttpStatusCode.NotFound, Forecasts = null };
                    }

                    return new WeatherResponse
                    {
                        StatusCode = HttpStatusCode.OK,
                        Forecasts = apiResponse.Content.Forecasts
                    };
                }
            } catch(Exception ex) when ((ex is ApiException) || (ex is WebException))
            {
                // Here we would Log exception
                return new WeatherResponse { StatusCode = HttpStatusCode.InternalServerError, Forecasts = null };
            }
        }
    }
}
