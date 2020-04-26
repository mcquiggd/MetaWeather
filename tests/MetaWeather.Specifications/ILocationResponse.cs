using System.Collections.Generic;

namespace MetaWeather.Specifications
{
    public interface ILocationResponse
    {
        List<ILocation> Locations { get; set; }
    }
}
