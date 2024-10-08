﻿using AutoMapper;
using Microsoft.Extensions.Options;
using mlt.common.dtos.synology;
using mlt.common.extensions;
using mlt.common.options;
using mlt.synology.clients.dtos;

namespace mlt.synology.clients;

internal class FileStationHttpClient(IOptions<SynologyOptions> options, IMapper mapper)
    : SynologyHttpClient(options.Value, "webapi/entry.cgi"), IFileStationHttpClient
{
    private const string ListApi = "SYNO.FileStation.List";
    private const string CreateFolderApi = "SYNO.FileStation.CreateFolder";

    public async Task<List<SynoFolder>> GetFoldersWithSubs(string? folderPath = null)
    {
        string method, parameters = string.Empty;

        if (string.IsNullOrWhiteSpace(folderPath))
            method = "list_share";
        else
        {
            method = "list";
            parameters = $"&folder_path={folderPath}";
        }

        var response = await GetSynoAsync<SynoResponse>(ListApi, "2", method, parameters);

        var fileItems = string.IsNullOrWhiteSpace(folderPath)
            ? response.Data.Shares.Where(x => x.IsDir && options.Value.SharedFoldersList.Any(y => y == x.Name))
            : response.Data.Files.Where(x => x.IsDir);

        var synoFolders = mapper.Map<IEnumerable<SynoFolder>>(fileItems).ToList();

        foreach (var folder in synoFolders)
            folder.Folders.AddRange(await GetFoldersWithSubs(folder.Path.ToUrlSafeString()));

        return synoFolders;
    }

    public async Task<bool> CreateFolder(string folderPath, string folderName)
        => (await GetSynoAsync<SynoResponse>(CreateFolderApi, "2", "create", $"&folder_path={folderPath.ToUrlSafeString()}&name={folderName.RemoveUnsafeFolderCharacters()}"))
           .Success;
}