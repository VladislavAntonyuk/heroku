using Microsoft.EntityFrameworkCore;

namespace drawgo;

public class WeatherForecastContext : DbContext
{
    public WeatherForecastContext(DbContextOptions options):base(options)
    {
    }

    public DbSet<WeatherForecast> Forecasts { get; set; }
}
