using AutoMapper;
using MoviesAPI.Core.Entities;
using MoviesAPI.Dtos.Users;

namespace MoviesAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserGetDto>().ReverseMap();
            
        }
    }
}
