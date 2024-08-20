using Microsoft.Extensions.Options;

namespace mlt.common.options;

public class SynologyOptions : IOptions<SynologyOptions>
{
    public string BaseUrl { get; set; } = null!;
    public string Account { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Sid { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string SharedFolders { get; set; } = null!;
    public List<string> SharedFoldersList => SharedFolders.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

    public SynologyOptions Value => this;
}