using SynoTask = mlt.dtos.synology.SynoTask;

namespace mlt.synology.services;

internal class DownloadStationService(IDownloadStationHttpClient dsClient) : IDownloadStationService
{
    public async Task<IEnumerable<SynoTask>> GetTasks()
        => await dsClient.GetTasks();

    public Task CreateTask(IEnumerable<string> uri, string? destination = "Movies")
        => Task.WhenAll(uri.Select(link => dsClient.CreateTask(link, destination!)).ToArray());
}