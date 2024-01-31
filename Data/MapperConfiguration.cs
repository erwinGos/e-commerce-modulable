using AutoMapper;
using Data.DTO.User;
using Database.Entities;

namespace Data
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            CreateMap<SignUpUser, User>();
        }
    }
}
