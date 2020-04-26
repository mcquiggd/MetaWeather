using System.Collections.Generic;

namespace MetaWeather.Core.Interfaces
{
    public interface ILocationResponse
    {
        List<ILocation> Locations  { get; set; }
        int             StatusCode { get; set; }
    }
}