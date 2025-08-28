using AdvertPlatforms.Api.Models;
using System.Text;

namespace AdvertPlatforms.Api.Services
{
    public class PlatformRepository : IPlatformRepository
    {
        private Dictionary<string, HashSet<string>> _index = new(StringComparer.OrdinalIgnoreCase);
        private readonly object _lock = new();

        public Task<UploadResult> LoadFromStreamAsync(Stream fileStream)
        {
            using var reader = new StreamReader(fileStream, Encoding.UTF8, leaveOpen: false);
            var newIndex = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
            var platformsSet = new HashSet<string>(StringComparer.Ordinal);

            string? line;
            int lineNo = 0;
            int locCount = 0;

            while ((line = reader.ReadLine()) is not null)
            {
                lineNo++;
                line = line.Trim();
                if (string.IsNullOrEmpty(line))
                    continue; 

                var parts = line.Split(':', 2);
                if (parts.Length != 2)
                    throw new FormatException($"Строка {lineNo}: отсутствует разделитель ':'.");

                var name = parts[0].Trim();
                var locsRaw = parts[1].Trim();

                if (string.IsNullOrWhiteSpace(name))
                    throw new FormatException($"Строка {lineNo}: не указано название площадки.");

                if (string.IsNullOrWhiteSpace(locsRaw))
                    throw new FormatException($"Строка {lineNo}: не указан список локаций.");

                var locs = locsRaw.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                  .Select(l => NormalizeLocation(l))
                                  .ToList();

                if (locs.Count == 0)
                    throw new FormatException($"Строка {lineNo}: пустой список локаций.");

                foreach (var loc in locs)
                {
                    if (!loc.StartsWith('/'))
                        throw new FormatException($"Строка {lineNo}: локация '{loc}' должна начинаться с '/'.");

                    if (!newIndex.TryGetValue(loc, out var set))
                    {
                        set = new HashSet<string>(StringComparer.Ordinal);
                        newIndex[loc] = set;
                    }
                    set.Add(name);
                    locCount++;
                }

                platformsSet.Add(name);
            }

            lock (_lock)
            {
                _index = newIndex;
            }

            return Task.FromResult(new UploadResult(platformsSet.Count, locCount));
        }

        public List<string> SearchByLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
                return new();

            var normalized = NormalizeLocation(location);
            var result = new HashSet<string>(StringComparer.Ordinal);

            string current = normalized;
            while (true)
            {
                if (_index.TryGetValue(current, out var names))
                {
                    foreach (var n in names) result.Add(n);
                }

                int lastSlash = current.LastIndexOf('/');
                if (lastSlash <= 0) break;
                current = current.Substring(0, lastSlash);
            }

            return result.OrderBy(s => s, StringComparer.Ordinal).ToList();
        }

        public IEnumerable<AdvertisingPlatform> GetAll()
        {
            var map = new Dictionary<string, AdvertisingPlatform>(StringComparer.Ordinal);
            foreach (var (loc, names) in _index)
            {
                foreach (var n in names)
                {
                    if (!map.TryGetValue(n, out var p))
                    {
                        p = new AdvertisingPlatform { Name = n };
                        map[n] = p;
                    }
                    p.Locations.Add(loc);
                }
            }
            return map.Values;
        }

        private static string NormalizeLocation(string raw)
        {
            string s = raw.Trim();
            s = s.Replace('\\', '/');

            if (!s.StartsWith('/'))
                s = "/" + s;

            while (s.Contains("//"))
                s = s.Replace("//", "/");

            if (s.Length > 1 && s.EndsWith('/'))
                s = s.TrimEnd('/');

            return s;
        }
    }
}
