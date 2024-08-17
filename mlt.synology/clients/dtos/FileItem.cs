namespace mlt.synology.clients.dtos;

internal class FileItem
{
    public bool IsDir { get; set; }
    public string Name { get; set; } = null!;
    public string Path { get; set; } = null!;
}