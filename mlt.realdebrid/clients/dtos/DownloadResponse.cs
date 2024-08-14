namespace mlt.realdebrid.clients.dtos;

public class DownloadResponse
{
    public string Id { get; set; } = null!;
    public string Filename { get; set; } = null!;

    // public string MimeType { get; set; }
    // public long Filesize { get; set; }
    public string Link { get; set; } = null!;

    // public string Host { get; set; }
    // public string HostIcon { get; set; }
    // public int Chunks { get; set; }
    public string Download { get; set; } = null!;

    // public int Streamable { get; set; }
    public DateTime? Generated { get; set; } = null!;
}