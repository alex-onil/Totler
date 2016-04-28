using AutoMapper;
using TotlerRepository.Models.Identity;
using Trade_MVC6.Models.Account;
using Trade_MVC6.Models.Shared;

namespace Trade_MVC6.Mapper.Profiles
{
    public class ProfileViewModelProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ContactRecord, ContactViewModel>()
                    .ForSourceMember(m => m.User, opt => opt.Ignore());

            CreateMap<ContactViewModel, ContactRecord>();


            var map = CreateMap<ApplicationUser, ProfileViewModel>();
            map.ForAllMembers(opt => opt.Ignore());
            map.ForMember(m => m.Nickname, opt => opt.MapFrom(p => p.UserName))
                .ForMember(m => m.Email, opt => opt.MapFrom(p => p.Email))
                .ForMember(m => m.CompanyName, opt => opt.MapFrom(p => p.CompanyName))
                .ForMember(m => m.Contact, opt => opt.MapFrom(p => p.Contact))
                .ForMember(m => m.EmailConfirmed, opt => opt.MapFrom(p => p.EmailConfirmed));

            var revmap = CreateMap<ProfileViewModel, ApplicationUser>();
            revmap.ForAllMembers(opt => opt.Ignore());
            revmap.ForMember(m => m.UserName, opt => opt.MapFrom(f => f.Nickname))
                .ForMember(m => m.CompanyName, opt => opt.MapFrom(f => f.CompanyName))
                .ForMember(m => m.Contact, opt => opt.MapFrom(f => f.Contact));

        }
    }
}
