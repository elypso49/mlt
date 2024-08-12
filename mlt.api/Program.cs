using System.Text.Json;
using mlt.common.options;
using mlt.rss;
using mlt.synology;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true).AddEnvironmentVariables();

builder.Services.AddEndpointsApiExplorer()
       .AddSwaggerGen()
       .AddAutoMapper(typeof(MappingRssProfile))
       .AddAutoMapper(typeof(MappingSynoProfile))
       .AddCors(options => { options.AddPolicy("AllowAll", x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); })
       .Configure<MongoDbOptions>(builder.Configuration.GetSection(nameof(MongoDbOptions)))
       .Configure<SynologyOptions>(builder.Configuration.GetSection(nameof(SynologyOptions)))
       .GetRssDependencyInjection()
       .GetSynoDependencyInjection()
       .AddSingleton(new JsonSerializerOptions
                     {
                         PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                         PropertyNameCaseInsensitive = true
                     })
       .AddControllers()
       .AddJsonOptions(options =>
                       {
                           options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                           options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                       });


var app = builder.Build();

app.UseCors("AllowAll")
   .UseSwagger()
   .UseSwaggerUI(options =>
                 {
                     options.SwaggerEndpoint("/swagger/v1/swagger.json", "Mlt Station");
                     options.RoutePrefix = string.Empty; // Serve Swagger UI at root path
                 });

app.MapControllers();

app.Run();