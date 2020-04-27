using Newtonsoft.Json;

namespace MetaWeather.Core.Interfaces
{
    public interface ILocation
    {
        [JsonProperty("latt_long")]
        string LatLong { get; set; }

        [JsonProperty("location_type")]
        string LocationType { get; set; }

        [JsonProperty("title")]
        string Title { get; set; }

        [JsonProperty("woeid")]
        int WoeId { get; set; }
    }
}