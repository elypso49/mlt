namespace mlt.synology.dtos;

public enum DownloadStatus
{
    waiting,
    downloading,
    paused,
    finishing,
    finished,
    hash_checking,
    seeding,
    filehosting_waiting,
    extracting,
    error
}