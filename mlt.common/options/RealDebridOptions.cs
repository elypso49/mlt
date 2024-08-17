namespace mlt.common.options;

public class RealDebridOptions : IOptions<RealDebridOptions>
{
    public string ApiToken { get; init; } = null!;
    public string BaseUrl { get; set; } = null!;
    public string ExtensionFilter { get; set; } = null!;
    public List<string> ExtensionFilterList => ExtensionFilter.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

    public RealDebridOptions Value => this;
}