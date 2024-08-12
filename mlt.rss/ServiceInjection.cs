using Microsoft.Extensions.DependencyInjection;
using mlt.rss.datas;
using mlt.rss.services;

namespace mlt.rss;

public static class ServiceInjection
{
    public static IServiceCollection GetRssDependencyInjection(this IServiceCollection services)
        => services.AddScoped<IRssFeedProcessorRepository, RssFeedProcessorRepository>()
                   .AddScoped<IRssFeedRepository, RssFeedRepository>()
                   .AddScoped<IRssFeedResultRepository, RssFeedResultRepository>()
                   .AddScoped<IRssFeedProcessorService, RssFeedProcessorService>()
                   .AddScoped<IRssFeedResultService, RssFeedResultService>()
                   .AddScoped<IRssFeedService, RssFeedService>();
}