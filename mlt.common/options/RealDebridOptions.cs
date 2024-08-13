namespace mlt.common.options;

public class RealDebridOptions : IOptions<RealDebridOptions>
{
    public string ApiToken { get; init; } = null!;

    public RealDebridOptions Value => this;
}