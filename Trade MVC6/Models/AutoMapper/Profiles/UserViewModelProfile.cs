using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Trade_MVC6.Models.Identity;
using Trade_MVC6.Models.Identity.AccountDetails;
using Trade_MVC6.ViewModels.Account;
using Trade_MVC6.ViewModels.Admin;
using Trade_MVC6.ViewModels.Shared;

namespace Trade_MVC6.Models.AutoMapper.Profiles
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
