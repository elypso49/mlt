namespace mlt.synology.clients.dtos;

internal class SynoTaskResponse
{
    public string Id { get; set; } = null!;
    public long Size { get; set; }
    public string Status { get; set; }  = null!;
    public string Title { get; set; }  = null!;
    public string Type { get; set; }  = null!;
    public string Username { get; set; }  = null!;

    public Additional Additional { get; set; }  = null!;
}