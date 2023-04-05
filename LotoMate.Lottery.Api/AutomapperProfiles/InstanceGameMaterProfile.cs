using AutoMapper;
using LotoMate.Lottery.Api.ViewModels;
using LotoMate.Lottery.Infrastructure.Models;

namespace LotoMate.Lottery.Api.AutomapperProfiles
{
    public class InstanceGameMaterProfile : Profile
    {
        public InstanceGameMaterProfile()
        {
            CreateMap<InstanceGameMaster, InstanceGameMasterViewModel>(MemberList.Destination);
            CreateMap<InstanceGameMasterViewModel, InstanceGameMaster>(MemberList.Source);
        }
    }

    public class InstanceGameUpdateProfile : Profile
    {
        public InstanceGameUpdateProfile()
        {
            CreateMap<InstanceGameMaster, InstanceGameUpdateVM>(MemberList.Destination);
            CreateMap<InstanceGameUpdateVM, InstanceGameMaster>(MemberList.Source);
        }
    }
}
