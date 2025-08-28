using AdvertPlatforms.Api.Models;

namespace AdvertPlatforms.Api.Services
{
    public interface IPlatformRepository
    {
        /// <summary>
        /// Загружает данные из файла в память.
        /// Бросает FormatException при проблемах с форматом.
        /// </summary>
        Task<UploadResult> LoadFromStreamAsync(Stream fileStream);

        /// <summary>
        /// Возвращает имена площадок, подходящие для локации.
        /// </summary>
        List<string> SearchByLocation(string location);

        /// <summary>
        /// Для отладки/расширения.
        /// </summary>
        IEnumerable<AdvertisingPlatform> GetAll();
    }
}