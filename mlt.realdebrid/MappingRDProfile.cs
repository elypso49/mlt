namespace mlt.realdebrid;

public class MappingRdProfile : Profile
{
    public MappingRdProfile() => CreateMap<DownloadItem, RdFileInfo>();
}