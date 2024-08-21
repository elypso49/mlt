using AutoMapper;
using mlt.common.datas.dtos;
using mlt.common.dtos.rss;
using mlt.rss.repositories.models;
using MongoDB.Driver;

namespace mlt.rss;

public class MappingRssProfile : Profile
{
    public MappingRssProfile()
    {
        CreateMap<RssFeedModel, RssFeed>().ReverseMap();
        CreateMap<RssFeedResultModel, RssFeedResult>().ReverseMap();
        CreateMap<DeleteResult.Acknowledged, DeleteResponse>();
        CreateMap<ReplaceOneResult.Acknowledged, UpdateResponse>();
    }
}