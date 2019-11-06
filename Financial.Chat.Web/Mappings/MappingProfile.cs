using AutoMapper;
using Financial.Chat.Domain.ApiModels.Request;
using Financial.Chat.Domain.ApiModels.Response;
using Financial.Chat.Domain.Models;
using System;

namespace Financial.Chat.Web.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, LoggedInUserDto>();

            CreateMap<RegisterRequestDto, ApplicationUser>()
               .ForMember(x => x.LastLoginDate, y => y.MapFrom(z => DateTime.UtcNow))
               .ForMember(x => x.PasswordHash, y => y.MapFrom(z => z.Password));
        }
    }
}
