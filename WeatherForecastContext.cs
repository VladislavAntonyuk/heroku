using Microsoft.EntityFrameworkCore;

namespace drawgo;

public class WeatherForecastContext : DbContext
{
    public WeatherForecastContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var connUrl = Environment.GetEnvironmentVariable(env == "Development" ? "JAWSDB_URL": "CLEARDB_DATABASE_URL");
        var connStr = connUrl.Replace("mysql://", string.Empty);
        var userPassSide = connStr.Split("@")[0];
        var hostSide = connStr.Split("@")[1];

        var connUser = userPassSide.Split(":")[0];
        var connPass = userPassSide.Split(":")[1];
        var connHost = hostSide.Split("/")[0];
        var connDb = hostSide.Split("/")[1].Split("?")[0];

        connStr = $"server={connHost};Uid={connUser};Pwd={connPass};Database={connDb}";

        optionsBuilder.UseMySql(connStr, ServerVersion.AutoDetect(connStr));
    }

    public DbSet<WeatherForecast> Forecasts { get; set; }
}
