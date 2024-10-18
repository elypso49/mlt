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
    
    public long? Create_time { get; set; } = null!;
    public long? Completed_time { get; set; } = null!;
    public DateTime? CreatedDateTime => Create_time is null ? null : DateTimeOffset.FromUnixTimeSeconds(Create_time.Value).DateTime;
    public DateTime? CompletedDateTime => Completed_time is null ? null : DateTimeOffset.FromUnixTimeSeconds(Completed_time.Value).DateTime;
}