namespace mlt.synology.clients;

internal abstract class SynologyHttpClient(JsonSerializerOptions jsonSerializerOptions, SynologyOptions synologyOptions, string entrypoint)
    : HttpService(jsonSerializerOptions, synologyOptions.BaseUrl)
{
    // protected static string DownloadStationUri => "webapi/DownloadStation/task.cgi";
    // protected static string FileStationUri => "webapi/entry.cgi";

    private string ParamAuth => $"?_sid={synologyOptions.Sid}&SynoToken={synologyOptions.Token}";

    // protected virtual string ParamApi => throw new NotImplementedException(nameof(ParamApi));
    // protected string BaseDsApi => $"{DownloadStationUri}{ParamAuth}{ParamApi}";
    // protected string BaseFsApi => $"{FileStationUri}{ParamAuth}{ParamApi}";

    protected async Task<TResponse> GetSynoAsync<TResponse>(string api, string version, string method, string? parameters = null)
        => await GetAsync<TResponse>($"{entrypoint}{ParamAuth}&api={api}&version={version}&method={method}{(string.IsNullOrEmpty(parameters) ? "" : $"{parameters}")}");
}