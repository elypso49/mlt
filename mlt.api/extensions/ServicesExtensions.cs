using mlt.common.options;
using mlt.realdebrid;
using mlt.rss;
using mlt.synology;
using mlt.workflow;

namespace mlt.api.extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddCustonAutoMapper(this IServiceCollection services)
        => services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<MappingRssProfile>();
            cfg.AddProfile<MappingSynoProfile>();
            cfg.AddProfile<MappingRdProfile>();
        });

    public static IServiceCollection ConfigureCustomOptions(this IServiceCollection services, IConfiguration configuration)
        => services.Configure<MongoDbOptions>(configuration.GetSection(nameof(MongoDbOptions)))
            .Configure<SynologyOptions>(configuration.GetSection(nameof(SynologyOptions)))
            .Configure<RealDebridOptions>(configuration.GetSection(nameof(RealDebridOptions)));

    public static IServiceCollection AddCustomDependencies(this IServiceCollection services)
        => services.GetRssDependencyInjection()
            .GetSynoDependencyInjection()
            .GetRealDebdridDependencyInjection()
            .GetWorkflowDependencyInjection();
}