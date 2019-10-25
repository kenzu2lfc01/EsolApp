using AutoMapper;
using EsolApp.Data.Model;
using EsolApp.ViewModel;
using System.Collections.Generic;

namespace EsolApp.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Images, ImageViewModel>().ForMember(x => x.Base64Image, y => y.MapFrom(src => src.Base64Image))
                .ForMember(x => x.Id, y => y.MapFrom(src => src.Id)); 
        }
    }
}