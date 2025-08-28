using AdvertPlatforms.Api.Models;

namespace AdvertPlatforms.Api.Services
{
    public class PlatformRepository : IPlatformRepository
    {
        public IEnumerable<AdvertisingPlatform> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UploadResult> LoadFromStreamAsync(Stream fileStream)
        {
            throw new NotImplementedException();
        }

        public List<string> SearchByLocation(string location)
        {
            throw new NotImplementedException();
        }
    }
}
