using AutoMapper;
using TotlerRepository.Models.Identity;
using Trade_MVC6.Models.Admin;

namespace Trade_MVC6.Mapper.Profiles
{
    public class UserViewModelProfile : Profile
    {
        protected override void Configure()
        {

            var map = CreateMap<ApplicationUser, UserViewModel>();
            map.ForMember(m => m.Nickname, opt => opt.MapFrom(p => p.UserName));

            var revmap = CreateMap<UserViewModel, ApplicationUser>();
            revmap.ForMember(m => m.Email, opt => opt.Ignore());

        }
    }
}
