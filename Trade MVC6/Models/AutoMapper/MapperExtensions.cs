using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Trade_MVC6.Models.AutoMapper.Profiles;

namespace Trade_MVC6.Models.AutoMapper
{
    public static class MapperExtensions
    {
        public static void MapperConfigure(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                // ProfileViewModel
                cfg.AddProfile<ProfileViewModelProfile>();

                // RegisterViewModel
                cfg.AddProfile<RegisterViewModelProfile>();
            });
            var mapper = config.CreateMapper(); 
            services.AddInstance(typeof(IMapper), mapper);
        }
    }
}
