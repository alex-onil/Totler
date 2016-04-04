using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace Trade_MVC6.Models.AutoMapper
{
    public static class MapperExtensions
    {
        public static void MapperConfigure(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MainAppProfile());
            });
            var mapper = config.CreateMapper(); 
            services.AddInstance(typeof(IMapper), mapper);
        }
    }
}
