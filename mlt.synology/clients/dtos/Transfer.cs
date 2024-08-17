namespace mlt.synology.clients.dtos;

internal class Transfer
{
    public int DownloadedPieces { get; set; }
    public long SizeDownloaded { get; set; }
    public long SizeUploaded { get; set; }
    public int SpeedDownload { get; set; }
    public int SpeedUpload { get; set; }
}