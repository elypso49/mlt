﻿using AutoMapper;
using mlt.synology.datas.models;
using mlt.synology.dtos;

namespace mlt.synology;

public class MappingSynoProfile : Profile
{
    public MappingSynoProfile()
        => CreateMap<TaskDto, SynoTask>()
          .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.Additional.Detail.Destination))
          .ForMember(dest => dest.Uri, opt => opt.MapFrom(src => src.Additional.Detail.Uri))
          .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
          .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
          .ForMember(dest => dest.Status, opt => opt.MapFrom(src => MapStatus(src.Status)))
          .ForMember(dest => dest.size, opt => opt.MapFrom(src => src.Size))
          .ForMember(dest => dest.size_downloaded, opt => opt.MapFrom(src => src.Additional.Transfer.SizeDownloaded));

    private static DownloadStatus MapStatus(string status)
        => status.ToLower() switch
           {
               "waiting" => DownloadStatus.waiting,
               "downloading" => DownloadStatus.downloading,
               "paused" => DownloadStatus.paused,
               "finishing" => DownloadStatus.finishing,
               "finished" => DownloadStatus.finished,
               "hash_checking" => DownloadStatus.hash_checking,
               "seeding" => DownloadStatus.seeding,
               "filehosting_waiting" => DownloadStatus.filehosting_waiting,
               "extracting" => DownloadStatus.extracting,
               "error" => DownloadStatus.error,
               _ => DownloadStatus.error // Default to error if status is unrecognized
           };
}