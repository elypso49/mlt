namespace mlt.realdebrid.clients.dtos;

internal class DownloadResponse
{
    public string Id { get; set; } = null!;
    public string Filename { get; set; } = null!;
    public string Link { get; set; } = null!;
    public string Download { get; set; } = null!;
    public DateTime? Generated { get; set; } = null!;
}