namespace mlt.realdebrid;

public class MappingRdProfile : Profile
{
    public MappingRdProfile()
    {
        CreateMap<DownloadResponse, RealDebridTorrentInfo>()
           .ForMember(dest => dest.Progress, opt => opt.Ignore())
           .ForMember(dest => dest.Status, opt => opt.Ignore())
           .ForMember(dest => dest.Links, opt => opt.Ignore())
           .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.Generated));

        CreateMap<TorrentModel, RealDebridTorrentInfo>()
           .ForMember(dest => dest.Link, opt => opt.Ignore())
           .ForMember(dest => dest.Download, opt => opt.Ignore())
           .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.Added));

        CreateMap<TorrentFile, RealDebridFileInfo>();
    }
}