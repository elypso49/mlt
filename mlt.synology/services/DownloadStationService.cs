using SynoTask = mlt.dtos.synology.SynoTask;

namespace mlt.synology.services;

internal class DownloadStationService(IDownloadStationHttpClient dsClient) : IDownloadStationService
{
    public async Task<IEnumerable<SynoTask>> GetTasks()
        => await dsClient.GetTasks();

    public async Task<List<(string uri, bool isSuccess)>> CreateTask(IEnumerable<string> uri, string? destination = "Movies")
    {
        var result = new List<(string uri, bool isSuccess)>();

        foreach (var uriItem in uri)
            result.Add(await dsClient.CreateTask(uriItem, destination!));

        return result;
    }
}