﻿using System.Text.Json;
using Microsoft.Extensions.Options;
using mlt.common.datas;
using mlt.common.options;

namespace mlt.synology.datas;

public abstract class SynologyHttpClient(IOptions<SynologyOptions> options, JsonSerializerOptions jsonSerializerOptions) : HttpService(jsonSerializerOptions)
{
    private SynologyOptions SynologyOptions => options.Value;

    private string DownloadStationUri => $"{SynologyOptions.Endpoint}/webapi/DownloadStation/task.cgi";

    private string ParamAuth => $"?_sid={SynologyOptions.Sid}&SynoToken={SynologyOptions.Token}";

    protected virtual string ParamApi => throw new NotImplementedException(nameof(ParamApi));

    protected string BaseDsApi => $"{DownloadStationUri}{ParamAuth}{ParamApi}";
}