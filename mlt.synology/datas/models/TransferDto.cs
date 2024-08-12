namespace mlt.synology.datas.models;

public class TransferDto
{
    public int DownloadedPieces { get; set; }
    public long SizeDownloaded { get; set; }
    public long SizeUploaded { get; set; }
    public int SpeedDownload { get; set; }
    public int SpeedUpload { get; set; }
}