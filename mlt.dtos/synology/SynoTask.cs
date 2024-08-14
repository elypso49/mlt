namespace mlt.dtos.synology;

public class SynoTask
{
    public string Destination { get; set; }

    public string Uri { get; set; }

    public string Title { get; set; }

    public string Id { get; set; }

    public DownloadStatus Status { get; set; }

    public long size { get; set; }

    public long size_downloaded { get; set; }
}