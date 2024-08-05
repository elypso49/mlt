using Microsoft.Extensions.DependencyInjection;
using mlt.dal.repositories;

namespace mlt.dal;

public static class ServiceInjection
{
    public static IServiceCollection GetDependencyInjection(IServiceCollection services)
        => services.AddScoped<IRssFeedProcessorRepository, RssFeedProcessorRepository>()
                   .AddScoped<IRssFeedRepository, RssFeedRepository>()
                   .AddScoped<IRssFeedResultRepository, RssFeedResultRepository>();
}