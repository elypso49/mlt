using mlt.common.datas;
using mlt.common.options;

namespace mlt.synology.clients;

internal abstract class SynologyHttpClient(SynologyOptions synologyOptions, string entrypoint) : HttpService(synologyOptions.BaseUrl)
{
    private string ParamAuth => $"?_sid={synologyOptions.Sid}&SynoToken={synologyOptions.Token}";

    protected async Task<TResponse?> GetSynoAsync<TResponse>(string api, string version, string method, string? parameters = null)
        where TResponse : class
        => await GetAsync<TResponse>($"{entrypoint}{ParamAuth}&api={api}&version={version}&method={method}{(string.IsNullOrEmpty(parameters) ? "" : $"{parameters}")}");
}