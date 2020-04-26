using System.Collections.Generic;
using System.Threading.Tasks;
using MetaWeather.Core.Entities;
using MetaWeather.Core.Interfaces;

namespace MetaWeather.Application
{
    public class ApiProxy : IApiProxy
    {
        public async Task<ILocationResponse> SubmitLocationRequest(ILocationRequest locationRequest)
        {
            var locations = new List<ILocation>();
            var location  = new Location {Title = "Belfast", LocationType = "City", Woeid = 44544};
            locations.Add(location);

            return await Task.FromResult(new LocationResponse
                {StatusCode = 200, Locations = locations});
        }
    }
}