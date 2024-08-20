using mlt.common.dtos.synology.enums;

namespace mlt.common.dtos.synology;

public class SynoTask
{
    public string Destination { get; set; } = null!;
    public string Uri { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Id { get; set; } = null!;
    public DownloadStatus Status { get; set; }
    public long size { get; set; }
    public long size_downloaded { get; set; }
}