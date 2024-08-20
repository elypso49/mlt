namespace mlt.synology.clients.dtos;

internal class Detail
{
    public long CompletedTime { get; set; }
    public int ConnectedLeechers { get; set; }
    public int ConnectedPeers { get; set; }
    public int ConnectedSeeders { get; set; }
    public long CreateTime { get; set; }
    public string Destination { get; set; }  = null!;
    public int SeedElapsed { get; set; }
    public long StartedTime { get; set; }
    public int TotalPeers { get; set; }
    public int TotalPieces { get; set; }
    public string UnzipPassword { get; set; }  = null!;
    public string Uri { get; set; }  = null!;
    public int WaitingSeconds { get; set; }
}