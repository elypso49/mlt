using mlt.workflow;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.Development.json", true, true).AddEnvironmentVariables();

builder.Services.AddEndpointsApiExplorer()
       .AddSwaggerGen()
       .AddAutoMapper(typeof(MappingRssProfile))
       .AddAutoMapper(typeof(MappingSynoProfile))
       .AddAutoMapper(typeof(MappingRdProfile))
       .AddCors(options => { options.AddPolicy("AllowAll", x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); })
       .Configure<MongoDbOptions>(builder.Configuration.GetSection(nameof(MongoDbOptions)))
       .Configure<SynologyOptions>(builder.Configuration.GetSection(nameof(SynologyOptions)))
       .Configure<RealDebridOptions>(builder.Configuration.GetSection(nameof(RealDebridOptions)))
       .GetRssDependencyInjection()
       .GetSynoDependencyInjection()
       .GetRealDebdridDependencyInjection()
       .GetWorkflowDependencyInjection()
       .AddSingleton(new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true })
       .AddControllers();

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