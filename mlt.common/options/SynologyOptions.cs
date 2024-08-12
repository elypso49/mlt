using Microsoft.Extensions.Options;

namespace mlt.common.options;

public class SynologyOptions : IOptions<SynologyOptions>
{
    public string Endpoint { get; set; } = null!;

    public string Account { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Sid { get; set; } = null!;

    public string Token { get; set; } = null!;

    public SynologyOptions Value => this;
}