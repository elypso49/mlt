using Microsoft.Extensions.FileProviders;
using mlt.api.extensions;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.Development.json", true, true).AddEnvironmentVariables();

builder.Services.AddEndpointsApiExplorer()
       .AddSwaggerGen()
       .AddSwaggerGenNewtonsoftSupport()
       .AddCors(options => { options.AddPolicy("AllowAll", x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); })
       .AddCustonAutoMapper()
       .ConfigureCustomOptions(builder.Configuration)
       .AddCustomDependencies()
       .AddControllers()
       .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));

builder.WebHost.ConfigureKestrel(serverOptions =>
                                 {
                                     serverOptions.Limits.MaxRequestBodySize = 52428800; // Set the max size to 50 MB (adjust as needed)
                                 });

var app = builder.Build();

var uploadDirectory = "/app/Uploads";

if (!Directory.Exists(uploadDirectory))
    Directory.CreateDirectory(uploadDirectory); // Ensure the upload directory exists

// Serve static files from the "Uploads" directory
app.UseStaticFiles(new StaticFileOptions
                   {
                       FileProvider = new PhysicalFileProvider(uploadDirectory),
                       RequestPath = "/uploads",
                       ServeUnknownFileTypes = true,
                       DefaultContentType = "application/octet-stream"
                   });

app.UseCors("AllowAll")
   .UseSwagger()
   .UseSwaggerUI(options =>
                 {
                     options.SwaggerEndpoint("/swagger/v1/swagger.json", "Mlt Station");
                     options.RoutePrefix = string.Empty;
                 });

app.MapControllers();
app.Run();