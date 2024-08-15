namespace mlt.synology.clients;

internal abstract class SynologyHttpClient(JsonSerializerOptions jsonSerializerOptions, IOptions<SynologyOptions> options) : HttpService(jsonSerializerOptions, options.Value.BaseUrl)
{
    private SynologyOptions SynologyOptions => options.Value;

    private string DownloadStationUri => "webapi/DownloadStation/task.cgi";
    private string FileStationUri => "webapi/entry.cgi";

    private string ParamAuth => $"?_sid={SynologyOptions.Sid}&SynoToken={SynologyOptions.Token}";

    protected virtual string ParamApi => throw new NotImplementedException(nameof(ParamApi));

    protected string BaseDsApi => $"{DownloadStationUri}{ParamAuth}{ParamApi}";
    protected string BaseFsApi => $"{FileStationUri}{ParamAuth}{ParamApi}";
}