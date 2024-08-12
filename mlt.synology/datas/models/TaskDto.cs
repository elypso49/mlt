namespace mlt.synology.datas.models;

public class TaskDto
{
    public string Id { get; set; }
    public long Size { get; set; }
    public string Status { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public string Username { get; set; }
    public AdditionalDto Additional { get; set; }
}