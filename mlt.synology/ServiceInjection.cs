using Microsoft.Extensions.DependencyInjection;
using mlt.synology.datas;
using mlt.synology.services;

namespace mlt.synology;

public static class ServiceInjection
{
    public static IServiceCollection GetSynoDependencyInjection(this IServiceCollection services)
        => services.AddScoped<IDownloadStationHttpClient, DownloadStationHttpClient>()
                   .AddScoped<IDownloadStationService, DownloadStationService>();
}