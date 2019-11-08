using AutoMapper;
using FinancialChat.Domain.ApiModels.Request;
using FinancialChat.Domain.ApiModels.Response;
using FinancialChat.Domain.Models;
using System;

namespace FinancialChat.Web.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, LoggedInUserDto>();

            CreateMap<Message, MessageDto>()
                .ForMember(x => x.UserName, y => y.MapFrom(z => z.SenderUser.UserName));

            CreateMap<RegisterRequestDto, ApplicationUser>()
               .ForMember(x => x.LastLoginDate, y => y.MapFrom(z => DateTime.UtcNow))
               .ForMember(x => x.PasswordHash, y => y.MapFrom(z => z.Password));
        }
    }
}
