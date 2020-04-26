using System.Collections.Generic;
using MetaWeather.Core.Interfaces;

namespace MetaWeather.Core.Entities
{
    public class LocationResponse : ILocationResponse
    {
        public List<ILocation> Locations  { get; set; } = new List<ILocation>();
        public int             StatusCode { get; set; }
    }
}