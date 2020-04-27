using System.Collections.Generic;
using System.Net;
using MetaWeather.Core.Interfaces;

namespace MetaWeather.Core.Entities
{
    public class LocationResponse : ILocationResponse
    {
        public List<Location> Locations { get; set; } = new List<Location>();

        public HttpStatusCode StatusCode { get; set; }
    }
}