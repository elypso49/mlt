namespace mlt.synology.datas;

public abstract class SynologyHttpClient(JsonSerializerOptions jsonSerializerOptions, IOptions<SynologyOptions> options) : HttpService(jsonSerializerOptions)
{
    private SynologyOptions SynologyOptions => options.Value;

    private string DownloadStationUri => $"{SynologyOptions.Endpoint}/webapi/DownloadStation/task.cgi";

    private string ParamAuth => $"?_sid={SynologyOptions.Sid}&SynoToken={SynologyOptions.Token}";

    protected virtual string ParamApi => throw new NotImplementedException(nameof(ParamApi));

    protected string BaseDsApi => $"{DownloadStationUri}{ParamAuth}{ParamApi}";
}