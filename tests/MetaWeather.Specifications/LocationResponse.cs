using System.Collections.Generic;

namespace MetaWeather.Specifications
{
    internal class LocationResponse : ILocationResponse
    {
        public List<ILocation> Locations { get; set; } = new List<ILocation>();
    }
}