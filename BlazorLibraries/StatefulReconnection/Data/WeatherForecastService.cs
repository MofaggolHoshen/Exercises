using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StatefulReconnection.Data
{

    public class WeatherForecastService
    {
      

        private static readonly string[] _summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild",
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        public WeatherForecastService(IMemoryCache memoryCache)
        {
            MemoryCache = memoryCache;
        }

        public IMemoryCache MemoryCache { get; }

        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            return MemoryCache.GetOrCreateAsync(startDate, async e =>
            {
                e.SetOptions(new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow =
                        TimeSpan.FromSeconds(30)
                });

                var rng = new Random();

                await Task.Delay(TimeSpan.FromSeconds(10));

                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = startDate.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = _summaries[rng.Next(_summaries.Length)]
                }).ToArray();
            });
        }
    }
}
