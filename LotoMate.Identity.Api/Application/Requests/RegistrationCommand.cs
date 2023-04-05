using LotoMate.Identity.API.ViewModels;
using MediatR;

namespace Identity.API.Application.Command
{
    public class RegistrationCommand : IRequest<UserViewModel>
    {
        public UserViewModel Users { get; set; }
        
    }
}
