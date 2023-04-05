using AutoMapper;
using LotoMate.Identity.API.ViewModels;
using LotoMate.Identity.Infrastructure.Models;

namespace LotoMate.Identity.API.AutoMapperProfiles
{
    public class ClientAutoMapperProfile : Profile
    {
        public ClientAutoMapperProfile()
        {
            CreateMap<RBAClient, RBAClientViewModel>(MemberList.Destination);
            //Used MapPath for reverse mapping and generating Contact object for email
            CreateMap<RBAClientViewModel, RBAClient>(MemberList.Source)
                    .ForPath(x => x.Contact.Email1,
                        m => m.MapFrom(x => x.EmailId));

        }
    }
}
