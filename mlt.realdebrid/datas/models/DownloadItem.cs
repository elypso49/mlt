namespace mlt.realdebrid.datas.models;

public class DownloadItem
{
    public string Id { get; set; }
    public string Filename { get; set; }
    public string MimeType { get; set; }
    public long Filesize { get; set; }
    public string Link { get; set; }
    public string Host { get; set; }
    public string HostIcon { get; set; }
    public int Chunks { get; set; }
    public string Download { get; set; }
    public int Streamable { get; set; }
    public DateTime Generated { get; set; }
}