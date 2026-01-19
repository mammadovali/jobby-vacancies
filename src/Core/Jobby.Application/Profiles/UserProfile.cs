using AutoMapper;
using Jobby.Application.Features.Queries.User.DTOs;

namespace Jobby.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Domain.Entities.Identity.User, UserDto>();
        }
    }
}
