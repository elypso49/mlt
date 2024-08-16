namespace mlt.realdebrid.clients.dtos;

internal class TorrentModel
{
    public string Id { get; set; }= null!;
    public string Filename { get; set; }= null!;
    // public string Hash { get; set; }= null!;
    // public long Bytes { get; set; }
    // public string Host { get; set; }= null!;
    // public int Split { get; set; }
    public double Progress { get; set; }  //from 0 to 100
    public string Status { get; set; } = null!; //magnet_error, magnet_conversion, waiting_files_selection, queued, downloading, downloaded, error, virus, compressing, uploading, dead
    public List<TorrentFile>? Files { get; set; } = [];
    public DateTime? Added { get; set; }
    public List<string>? Links { get; set; } = [];
    public DateTime? Ended { get; set; }
}