namespace MetaWeather.Specifications
{
    internal class Location : ILocation
    {
        public string Title { get; set; }
        public string LocationType { get; set; }
        public int Woeid { get; set; }
        public string LattLong { get; set; }
    }
}