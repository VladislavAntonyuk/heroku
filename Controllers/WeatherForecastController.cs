using Microsoft.AspNetCore.Mvc;

namespace drawgo.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly WeatherForecastContext context;

    public WeatherForecastController(WeatherForecastContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return context.Forecasts.ToList();
    }


    [HttpPost]
    public WeatherForecast Post(WeatherForecast weatherForecast)
    {
        context.Forecasts.Add(weatherForecast);
        return weatherForecast;
    }
}
