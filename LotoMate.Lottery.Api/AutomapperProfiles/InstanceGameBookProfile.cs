using AutoMapper;
using LotoMate.Lottery.Api.ViewModels;
using LotoMate.Lottery.Infrastructure.Models;

namespace LotoMate.Lottery.Api.AutomapperProfiles
{
    public class InstanceGameBookProfile : Profile
    {
        public InstanceGameBookProfile()
        {
            CreateMap<InstanceGameBook, InstanceGameBookViewModel>(MemberList.Destination)
                .ForMember(x => x.GameNumber, m => m.MapFrom(x => x.InstanceGame.GameNumber))
                .ForMember(x => x.Name, m => m.MapFrom(x => x.InstanceGame.Name));
            CreateMap<InstanceGameBookViewModel, InstanceGameBook>(MemberList.Source);

            //for Update and Insert GameBook
            CreateMap<InstanceGameBook, GameBookUpdateModel>(MemberList.Destination);
            CreateMap<GameBookUpdateModel, InstanceGameBook>(MemberList.Source);
        }
    }
}
