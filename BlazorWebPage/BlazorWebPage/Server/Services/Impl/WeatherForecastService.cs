using BlazorWebPage.Server.Services.Interfaces;
using BlazorWebPage.Shared;

namespace BlazorWebPage.Server.Services.Impl
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private static readonly string[] Summaries = new[]
{
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecast[] getAll()
        {
            return Enumerable.Range(1, 15).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 45),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
          .ToArray();
        }
    }
}
