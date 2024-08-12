namespace mlt.synology.datas.models;

public class DataDto
{
    public int Offset { get; set; }
    public List<TaskDto> Tasks { get; set; }
    public int Total { get; set; }
}