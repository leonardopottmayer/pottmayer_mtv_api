using AutoMapper;
using Pottmayer.MTV.Core.Domain.Modules.Auth.Dtos.Logic;
using Pottmayer.MTV.Core.Domain.Modules.Auth.Dtos.Rest;

namespace Pottmayer.MTV.Core.Domain.Modules.Auth.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterUserInputDto, RegisterUserRequestDto>()
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.PasswordConfirmation, opt => opt.MapFrom(src => src.PasswordConfirmation))
                .ReverseMap();

            CreateMap<LoginUserInputDto, LoginUserRequestDto>()
                .ReverseMap()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ReverseMap();
        }
    }
}
