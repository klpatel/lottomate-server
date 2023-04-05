using AutoMapper;
using LotoMate.Client.Api.ViewModels;
using LotoMate.Client.Infrastructure.Models;

namespace iTrack.App.API.AutoMapperProfiles
{
    public class RBAClientProfile : Profile
    {
        public RBAClientProfile()
        {
            CreateMap<RBAClient, RBAClientViewModel>(MemberList.Destination);
            CreateMap<RBAClientViewModel, RBAClient>(MemberList.Source);
            
        }
    }
    
}
