using AutoMapper;
using TotlerRepository.Models.Identity;
using Trade_MVC6.Models.Account;

namespace Trade_MVC6.Mapper.Profiles
{
    public class RegisterViewModelProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<RegisterViewModel, ContactRecord>();

            var map = CreateMap<RegisterViewModel, ApplicationUser>();
            map.ForAllMembers(opt => opt.Ignore());
            map.ForMember(m => m.UserName, opt => opt.MapFrom(p => p.Nickname))
                .ForMember(m => m.Email, opt => opt.MapFrom(p => p.Email))
                .ForMember(m => m.Contact, opt => opt.MapFrom(p => p))
                .ForMember(m => m.CompanyName, opt => opt.MapFrom(p => p.CompanyName));
            }
    }
}
