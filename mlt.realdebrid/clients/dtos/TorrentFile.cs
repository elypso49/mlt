namespace mlt.realdebrid.clients.dtos;

internal class TorrentFile
{
    public int Id { get; set; }
    public string Path { get; set; } = null!;
    public int Selected { get; set; }
}