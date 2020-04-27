
namespace MetaWeather.Api.Models
{
    public class ApiOptions
    {
        public string ApiUrl { get; set; }

        public string Audience { get; set; }

        public string Authority { get; set; }

        public int MaxRecords { get; set; } = 5;
    }
}