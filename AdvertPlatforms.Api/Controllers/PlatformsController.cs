using AdvertPlatforms.Api.Models;
using AdvertPlatforms.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdvertPlatforms.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _repository;
        private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase) { ".txt" };

        public PlatformsController(IPlatformRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Загружает текстовый файл с данными
        /// Формат строк: Название: /loc1, /loc2, ...
        /// </summary>
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(UploadResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest("Файл не передан или пуст.");

            var ext = Path.GetExtension(file.FileName);
            if (string.IsNullOrWhiteSpace(ext) || !AllowedExtensions.Contains(ext))
                return BadRequest("Поддерживаются только файлы с расширением .txt");

            try
            {
                using var stream = file.OpenReadStream();
                var result = await _repository.LoadFromStreamAsync(stream);
                return Ok(result);
            }
            catch (FormatException fe)
            {
                return BadRequest($"Ошибка формата данных: {fe.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Не удалось загрузить файл: {ex.Message}");
            }
        }

        /// <summary>
        /// Поиск площадок.
        /// </summary>
        [HttpGet("search")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Search([FromQuery] string location)
        {
            if (string.IsNullOrWhiteSpace(location))
                return BadRequest("Параметр 'location' обязателен.");

            if (!location.StartsWith('/'))
                return BadRequest("Локация должна начинаться с '/'.");

            var list = _repository.SearchByLocation(location);
            return Ok(list);
        }
    }
}
