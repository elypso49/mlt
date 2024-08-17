namespace mlt.realdebrid;

public static class ServiceInjection
{
    public static IServiceCollection GetRealDebdridDependencyInjection(this IServiceCollection services)
        => services.AddScoped<IRealDebridHttpClient, RealDebridHttpClient>().AddScoped<IRealDebridService, RealDebridService>();
}