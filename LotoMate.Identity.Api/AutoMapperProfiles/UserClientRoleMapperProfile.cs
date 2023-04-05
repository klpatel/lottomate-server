using AutoMapper;
using LotoMate.Identity.Infrastructure.Models;

namespace LotoMate.Identity.API.AutoMapperProfiles
{
    public class UserClientRoleMapperProfile : Profile
    {
        public UserClientRoleMapperProfile()
        {
            CreateMap<UserClientRole, UserClientRoleViewModel>(MemberList.Destination);
            CreateMap<UserClientRoleViewModel, UserClientRole>(MemberList.Source);
            
        }
    }
}
