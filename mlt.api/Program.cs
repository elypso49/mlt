using mlt.dal;
using mlt.dal.dbSettings;
using ServiceInjection = mlt.services.ServiceInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
                                       {
                                           var env = hostingContext.HostingEnvironment;

                                           config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                                 .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true);

                                           config.AddEnvironmentVariables();
                                       });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(MappingProfile));

ServiceInjection.GetDependencyInjection(builder.Services);
mlt.dal.ServiceInjection.GetDependencyInjection(builder.Services);

// builder.Services.Configure<MongoDbOptions>(options => builder.Configuration.GetSection(nameof(MongoDbOptions)).Bind(options));
builder.Services.Configure<MongoDbOptions>(builder.Configuration.GetSection(nameof(MongoDbOptions)));

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
                 {
                     options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                     options.RoutePrefix = string.Empty; // Serve Swagger UI at root path
                 });

app.MapControllers();

app.Run();

// var summaries = new[]
//                 {
//                     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//                 };
//
// app.MapGet("/weatherforecast",
//            () =>
//            {
//                var forecast = Enumerable.Range(1, 5)
//                                         .Select(index => new WeatherForecast(DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//                                                                              Random.Shared.Next(-20, 55),
//                                                                              summaries[Random.Shared.Next(summaries.Length)]))
//                                         .ToArray();
//
//                return forecast;
//            })
//    .WithName("GetWeatherForecast")
//    .WithOpenApi();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }