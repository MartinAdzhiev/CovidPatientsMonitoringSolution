using Application.Dtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PatientMeasure, PatientMeasureResponse>();

            CreateMap<Device, DeviceResponse>()
                .ForMember(dest => dest.PatientMeasuresResponses, opt => opt.MapFrom(src => src.PatientMeasures));

            CreateMap<Warning, WarningResponse>()
                .ForMember(dest => dest.PatientMeasureResponse, opt => opt.MapFrom(src => src.PatientMeasure));
        }
    }
}
