namespace mlt.synology;

public static class ServiceInjection
{
    public static IServiceCollection GetSynoDependencyInjection(this IServiceCollection services)
        => services.AddScoped<IDownloadStationHttpClient, DownloadStationHttpClient>()
                   .AddScoped<IDownloadStationService, DownloadStationService>()
                   .AddScoped<IFileStationHttpClient, FileStationHttpClient>()
                   .AddScoped<IFileStationService, FileStationService>();
}