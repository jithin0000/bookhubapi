using System.Linq;
using angu.Dtos;
using angu.models;
using AutoMapper;

namespace angu.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserForListDto>();
            CreateMap<User, UserDetail>()
            .ForMember(
                dest => dest.PhotoUrl , opt =>{
                    opt.MapFrom( src => src.Photos.FirstOrDefault(p => p.isMain).Url);
                }
            );
            CreateMap<Photo, PhotoDetail>();
        }
    }
}