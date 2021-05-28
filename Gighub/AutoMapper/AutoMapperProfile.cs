using AutoMapper;
using Gighub.Data;
using Gighub.Dtos;
using Gighub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gighub.AutoMapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<Source,Target>()
            CreateMap<GenreDto,Genre>();
            CreateMap<AppUser, AppUserDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
            CreateMap<Gig, GigDto>()
                 .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.AppUser)); 
            CreateMap<Notification, NotificationDto>()
                .ForMember(dest => dest.GigDto, opt => opt.MapFrom(src => src.Gig)); 
        }
    }
}
