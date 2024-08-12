using System.Collections;
using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Options;
using mlt.common.options;
using mlt.synology.datas.models;
using mlt.synology.dtos;

namespace mlt.synology.datas;

public class DownloadStationHttpClient(IOptions<SynologyOptions> options, JsonSerializerOptions jsonSerializerOptions , IMapper mapper) : SynologyHttpClient(options, jsonSerializerOptions), IDownloadStationHttpClient
{
    protected override string ParamApi => "&api=SYNO.DownloadStation.Task&version=1";
    
    public async Task<IEnumerable<SynoTask>> GetTasks()
    {
        var response = await GetAsync<ResponseDto>($"{BaseDsApi}&method=list&additional=detail,transfer,file,tracker,peer");
        
        var synoTasks = mapper.Map<IEnumerable<SynoTask>>(response.Data.Tasks);

        return synoTasks;
    }
}