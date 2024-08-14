namespace mlt.synology.clients.dtos;

public class Data
{
    public int Offset { get; set; }
    public List<TaskResponse> Tasks { get; set; }
    public int Total { get; set; }
}