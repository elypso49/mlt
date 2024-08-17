namespace mlt.dtos.synology;

public class SynoFolder
{
    public string Name { get; set; } = null!;
    public string Path { get; set; } = null!;
    public List<SynoFolder> Folders { get; set; } = [];
}