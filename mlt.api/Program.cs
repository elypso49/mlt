var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
                 {
                     options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                     options.RoutePrefix = string.Empty; // Serve Swagger UI at root path
                 });

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

app.MapControllers();

app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }