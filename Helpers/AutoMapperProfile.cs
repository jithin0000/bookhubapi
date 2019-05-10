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
        }
    }
}