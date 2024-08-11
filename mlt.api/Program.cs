using mlt.dal;
using mlt.dal.Options;
using ServiceInjection = mlt.services.ServiceInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true).AddEnvironmentVariables();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddCors(options => { options.AddPolicy("AllowAll", x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); });

ServiceInjection.GetDependencyInjection(builder.Services);
mlt.dal.ServiceInjection.GetDependencyInjection(builder.Services);

builder.Services.Configure<MongoDbOptions>(builder.Configuration.GetSection(nameof(MongoDbOptions)));

var app = builder.Build();
app.UseCors("AllowAll");

app.UseSwagger();

app.UseSwaggerUI(options =>
                 {
                     options.SwaggerEndpoint("/swagger/v1/swagger.json", "Mlt Station");
                     options.RoutePrefix = string.Empty; // Serve Swagger UI at root path
                 });

app.MapControllers();

app.Run();