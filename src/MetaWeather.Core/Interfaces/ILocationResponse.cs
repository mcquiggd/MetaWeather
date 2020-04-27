using System.Collections.Generic;
using System.Net;
using MetaWeather.Core.Entities;

namespace MetaWeather.Core.Interfaces
{
    public interface ILocationResponse
    {
        List<Location> Locations  { get; set; }
        HttpStatusCode StatusCode { get; set; }
    }
}