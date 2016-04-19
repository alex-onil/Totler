using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Trade_MVC6.Models.Identity;
using Trade_MVC6.Models.Identity.AccountDetails;
using Trade_MVC6.ViewModels.Account;
using Trade_MVC6.ViewModels.Shared;

namespace Trade_MVC6.Models.AutoMapper.Profiles
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
