namespace mlt.synology.clients;

internal class FileStationHttpClient(JsonSerializerOptions jsonSerializerOptions, IOptions<SynologyOptions> options, IMapper mapper)
    : SynologyHttpClient(jsonSerializerOptions, options), IFileStationHttpClient
{
    protected override string ParamApi => "&api=SYNO.FileStation.List&version=2";

    public async Task<List<SynoFolder>> GetFoldersWithSubs(string? folderPath = null)
    {
        string method, parameters = string.Empty;
        IEnumerable<FileItem> fileItems;

        if (string.IsNullOrWhiteSpace(folderPath))
            method = "list_share";
        else
        {
            method = "list";
            parameters = $"&folder_path={folderPath}";
        }

        var response = await GetAsync<SynoResponse>($"{BaseFsApi}&method={method}{parameters}");

        fileItems = string.IsNullOrWhiteSpace(folderPath)
            ? response.Data.Shares.Where(x => x.IsDir && options.Value.SharedFoldersList.Any(y => y == x.Name))
            : response.Data.Files.Where(x => x.IsDir);

        var synoFolders = mapper.Map<IEnumerable<SynoFolder>>(fileItems).ToList();

        foreach (var folder in synoFolders)
        {
            var path = folder.Path.Replace("/", "%2F");
            folder.Folders.AddRange(await GetFoldersWithSubs(path));
        }

        return synoFolders;
    }
}