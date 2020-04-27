using MetaWeather.Core.Interfaces;

namespace MetaWeather.Core.Entities
{
    public class Location : ILocation
    {
        public string LatLong { get; set; }

        public string LocationType { get; set; }

        public string Title { get; set; }

        public int WoeId { get; set; }
    }
}