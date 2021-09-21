using drawgo;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WeatherForecastContext>(x =>
{
    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    string connUrl;

    if (env == "Development")
    {
        connUrl = builder.Configuration.GetConnectionString("DemoConnection");
    }
    else
    {
        connUrl = Environment.GetEnvironmentVariable("CLEARDB_DATABASE_URL");
    }

    var connStr = connUrl.Replace("mysql://", string.Empty);
    var userPassSide = connStr.Split("@")[0];
    var hostSide = connStr.Split("@")[1];

    var connUser = userPassSide.Split(":")[0];
    var connPass = userPassSide.Split(":")[1];
    var connHost = hostSide.Split("/")[0];
    var connDb = hostSide.Split("/")[1].Split("?")[0];

    connStr = $"server={connHost};Uid={connUser};Pwd={connPass};Database={connDb}";

    x.UseMySQL(connStr);

});
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Draw & GO", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Draw & GO v1"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
