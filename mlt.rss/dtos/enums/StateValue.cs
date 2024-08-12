namespace mlt.rss.dtos.enums;

[Flags]
public enum StateValue
{
    Read = 1 << 0,       // 00001
    Debrided = 1 << 1,   // 00010
    Downloaded = 1 << 2, // 00100
    Archived = 1 << 3,   // 01000
    Pending = 1 << 4,
    UnRead = 1 << 5
}