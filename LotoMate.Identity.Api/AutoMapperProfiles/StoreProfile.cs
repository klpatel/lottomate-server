using AutoMapper;
using LotoMate.Identity.Api.ViewModels;
using LotoMate.Identity.Infrastructure.Models;

namespace LotoMate.Identity.Api.AutomapperProfiles
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<Store, StoreViewModel>(MemberList.Destination);
            CreateMap<StoreViewModel, Store>(MemberList.Source);

        }
    }
}
