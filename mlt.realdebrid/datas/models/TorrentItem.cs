namespace mlt.realdebrid.datas.models;

public class TorrentItem
{
    public string Id { get; set; }

    public string Filename { get; set; }

    public string Hash { get; set; }

    public long Bytes { get; set; }

    public string Host { get; set; }

    public int Split { get; set; }

    public int Progress { get; set; }

    public string Status { get; set; }

    public DateTime Added { get; set; }

    public List<string> Links { get; set; }

    public DateTime Ended { get; set; }
}