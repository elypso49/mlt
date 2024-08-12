﻿namespace mlt.synology.datas.models;

public class DetailDto
{
    public long CompletedTime { get; set; }
    public int ConnectedLeechers { get; set; }
    public int ConnectedPeers { get; set; }
    public int ConnectedSeeders { get; set; }
    public long CreateTime { get; set; }
    public string Destination { get; set; }
    public int SeedElapsed { get; set; }
    public long StartedTime { get; set; }
    public int TotalPeers { get; set; }
    public int TotalPieces { get; set; }
    public string UnzipPassword { get; set; }
    public string Uri { get; set; }
    public int WaitingSeconds { get; set; }
}