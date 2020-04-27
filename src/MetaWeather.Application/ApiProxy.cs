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
        private readonly IMetaweatherService _metaweatherService;

        public ApiProxy(IMetaweatherService metaweatherService)
        {
            _metaweatherService = metaweatherService;
        }

        public async Task<LocationResponse> SubmitLocationRequest(ILocationRequest locationRequest)
        {
            try
            {
                using (var apiResponse = await _metaweatherService.GetLocationByCityName(locationRequest.CityName))
                {
                    if (!apiResponse.IsSuccessStatusCode)
                        return await Task.FromResult(new LocationResponse
                            {StatusCode = apiResponse.StatusCode, Locations = null});

                    if (!apiResponse.Content.Any())
                        return await Task.FromResult(new LocationResponse
                            {StatusCode = HttpStatusCode.NotFound, Locations = null});

                    var content = apiResponse.Content;

                    var response = new LocationResponse();

                    response.StatusCode = HttpStatusCode.OK;
                    response.Locations  = content;

                    return await Task.FromResult(new LocationResponse
                        {StatusCode = HttpStatusCode.OK, Locations = apiResponse.Content});
                }
            }
            catch (Exception ex) when (ex is ApiException || ex is WebException)
            {
                //TODO:Log exception
                return await Task.FromResult(new LocationResponse
                    {StatusCode = HttpStatusCode.InternalServerError, Locations = null});
            }
        }
    }
}