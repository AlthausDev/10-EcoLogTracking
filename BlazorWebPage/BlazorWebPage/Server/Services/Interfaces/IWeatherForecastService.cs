using BlazorWebPage.Shared;

namespace BlazorWebPage.Server.Services.Interfaces
{
    public interface IWeatherForecastService
    {

        public WeatherForecast[] getAll();
    }
}
