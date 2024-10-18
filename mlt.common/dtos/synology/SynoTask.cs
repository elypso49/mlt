using mlt.common.dtos.synology.enums;

namespace mlt.common.dtos.synology;

public class SynoTask
{
    public string Destination { get; set; } = null!;
    public string Uri { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Id { get; set; } = null!;
    public DownloadStatus Status { get; set; }
    public long Size { get; set; }
    public long Size_downloaded { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public DateTime? CompletedDateTime { get; set; }
}