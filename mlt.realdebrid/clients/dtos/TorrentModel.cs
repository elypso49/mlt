namespace mlt.realdebrid.clients.dtos;

internal class TorrentModel
{
    public string Id { get; set; } = null!;
    public string Filename { get; set; } = null!;
    public double Progress { get; set; }
    public string Status { get; set; }
        = null!; //magnet_error, magnet_conversion, waiting_files_selection, queued, downloading, downloaded, error, virus, compressing, uploading, dead
    public List<TorrentFile>? Files { get; set; } = [];
    public DateTime? Added { get; set; }
    public List<string>? Links { get; set; } = [];
    public DateTime? Ended { get; set; }
}