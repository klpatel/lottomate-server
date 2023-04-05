using AutoMapper;
using LotoMate.Identity.API.ViewModels;
using LotoMate.Identity.Infrastructure.Models;

namespace LotoMate.Identity.API.AutoMapperProfiles
{
    public class UserAutoMapperProfile : Profile
    {
        public UserAutoMapperProfile()
        {
            CreateMap<User, UserViewModel>(MemberList.Destination);
            CreateMap<UserViewModel, User>(MemberList.Source);
        }
    }
    public class UserRoleMapperProfile : Profile
    {
        public UserRoleMapperProfile()
        {
            CreateMap<AspNetUserRole, UserClientRoleViewModel>(MemberList.Destination);
            CreateMap<UserClientRoleViewModel, AspNetUserRole>(MemberList.Source);

        }
    }
}
