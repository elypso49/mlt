namespace mlt.synology.clients.dtos;

internal class Data
{
    public int Offset { get; set; }
    public List<SynoTaskResponse> Tasks { get; set; } = [];
    public List<FileItem> Files { get; set; } = [];
    public List<FileItem> Shares { get; set; } = [];
    public int Total { get; set; }
}