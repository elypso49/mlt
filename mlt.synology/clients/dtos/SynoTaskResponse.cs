namespace mlt.synology.clients.dtos;

internal class SynoTaskResponse
{
    public string Id { get; set; }
    public long Size { get; set; }
    public string Status { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public string Username { get; set; }
    public Additional Additional { get; set; }
}