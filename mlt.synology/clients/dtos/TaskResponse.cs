namespace mlt.synology.clients.dtos;

public class TaskResponse
{
    public string Id { get; set; }
    public long Size { get; set; }
    public string Status { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public string Username { get; set; }
    public Additional Additional { get; set; }
}