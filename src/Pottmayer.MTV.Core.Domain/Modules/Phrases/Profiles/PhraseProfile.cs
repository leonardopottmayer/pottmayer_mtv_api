using AutoMapper;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Logic;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Dtos.Rest;
using Pottmayer.MTV.Core.Domain.Modules.Phrases.Entities;

namespace Pottmayer.MTV.Core.Domain.Modules.Phrases.Profiles
{
    public class PhraseProfile : Profile
    {
        public PhraseProfile()
        {
            CreateMap<PhraseDto, Phrase>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ReverseMap();

            CreateMap<CreatePhraseInputDto, CreatePhraseRequestDto>()
                .ReverseMap()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ReverseMap();
        }
    }
}
