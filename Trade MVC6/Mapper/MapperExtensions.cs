using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Trade_MVC6.Mapper.Profiles;

namespace Trade_MVC6.Mapper
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

                // UserViewModel
                cfg.AddProfile<UserViewModelProfile>();
            });
            var mapper = config.CreateMapper(); 
            services.AddInstance(typeof(IMapper), mapper);
        }
    }
}
