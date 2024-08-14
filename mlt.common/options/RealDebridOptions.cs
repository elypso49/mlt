namespace mlt.common.options;

public class RealDebridOptions : IOptions<RealDebridOptions>
{
    public string ApiToken { get; init; } = null!;
    public string BaseUrl { get; set; } = null!;

    public RealDebridOptions Value => this;
}