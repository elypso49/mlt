namespace mlt.dtos.realdebrid;

public class RealDebridTorrentInfo
{
    public string Id { get; init; } = null!;
    public string Filename { get; init; } = null!;
    public string Link { get; init; } = null!;
    public string Download { get; init; } = null!;
    public int? Progress { get; init; } = null!;
    public string Status { get; init; } = null!;
    public List<string> Links { get; init; } = null!;
    public DateTime? CreatedOn { get; init; } = null!;
    public List<RealDebridFileInfo> Files { get; set; } = [];
}