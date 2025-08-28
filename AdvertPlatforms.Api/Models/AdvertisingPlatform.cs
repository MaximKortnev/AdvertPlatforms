namespace AdvertPlatforms.Api.Models
{
    public class AdvertisingPlatform
    {
        public string Name { get; init; } = string.Empty;
        public List<string> Locations { get; init; } = new();
    }
}
