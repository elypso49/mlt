using mlt.api.extensions;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.Development.json", true, true)
    .AddEnvironmentVariables();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddSwaggerGenNewtonsoftSupport()
    .AddCors(options => { options.AddPolicy("AllowAll", x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); })
    .AddCustonAutoMapper()
    .ConfigureCustomOptions(builder.Configuration)
    .AddCustomDependencies()
    .AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));

var app = builder.Build();

app.UseCors("AllowAll")
    .UseSwagger()
    .UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Mlt Station");
        options.RoutePrefix = string.Empty;
    });

app.MapControllers();
app.Run();