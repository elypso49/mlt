using Microsoft.Extensions.DependencyInjection;
using mlt.common.services;

namespace mlt.services;

public static class ServiceInjection
{
    public static IServiceCollection GetDependencyInjection(IServiceCollection services)
        => services.AddScoped<IRssFeedProcessorService, RssFeedProcessorService>()
                   .AddScoped<IRssFeedResultService, RssFeedResultService>()
                   .AddScoped<IRssFeedService, RssFeedService>();
}