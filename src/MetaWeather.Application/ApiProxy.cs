using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MetaWeather.Core.Entities;
using MetaWeather.Core.Interfaces;
using Refit;

namespace MetaWeather.Application
{
    public class ApiProxy : IApiProxy
    {
        readonly IMetaWeatherService _metaWeatherService;

        public ApiProxy(IMetaWeatherService metaWeatherService) => _metaWeatherService = metaWeatherService;

        public async Task<LocationResponse> SubmitLocationRequest(ILocationRequest locationRequest)
        {
            try
            {
                using(var apiResponse = await _metaWeatherService.GetLocationByCityName(locationRequest.CityName)
                                                  .ConfigureAwait(false))
                {
                    if(!apiResponse.IsSuccessStatusCode)
                    {
                        return await Task.FromResult(new LocationResponse
                            { StatusCode = apiResponse.StatusCode, Locations = null })
                                         .ConfigureAwait(false);
                    }

                    if(!apiResponse.Content.Any())
                    {
                        return await Task.FromResult(new LocationResponse
                            { StatusCode = HttpStatusCode.NotFound, Locations = null })
                                         .ConfigureAwait(false);
                    }

                    return await Task.FromResult(new LocationResponse
                        { StatusCode = HttpStatusCode.OK, Locations = apiResponse.Content })
                                     .ConfigureAwait(false);
                }
            } catch(Exception ex) when ((ex is ApiException) || (ex is WebException))
            {
                //TODO:Log exception
                return await Task.FromResult(new LocationResponse
                    { StatusCode = HttpStatusCode.InternalServerError, Locations = null })
                                 .ConfigureAwait(false);
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
                        return await Task.FromResult(new WeatherResponse
                            { StatusCode = apiResponse.StatusCode, Forecasts = null })
                                         .ConfigureAwait(false);
                    }

                    var forecasts = apiResponse.Content.Forecasts;

                    if(!forecasts.Any())
                    {
                        return await Task.FromResult(new WeatherResponse
                            { StatusCode = HttpStatusCode.NotFound, Forecasts = null })
                                         .ConfigureAwait(false);
                    }

                    return await Task.FromResult(new WeatherResponse
                        { StatusCode = HttpStatusCode.OK, Forecasts = apiResponse.Content.Forecasts })
                                     .ConfigureAwait(false);
                }
            } catch(Exception ex) when ((ex is ApiException) || (ex is WebException))
            {
                //TODO:Log exception
                return await Task.FromResult(new WeatherResponse
                    { StatusCode = HttpStatusCode.InternalServerError, Forecasts = null })
                                 .ConfigureAwait(false);
            }
        }
    }
}