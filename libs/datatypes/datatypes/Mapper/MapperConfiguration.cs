using System;
using AutoMapper;
using DataTypes.Models;
using DataTypes.ViewModels;

namespace DataTypes.Mapper
{
    public static class MapperConfig
    {
        private static readonly MapperConfiguration MapperCfg;

        static MapperConfig()
        {
            MapperCfg = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ModelGroup, ModelGroupViewModel>();
                cfg.CreateMap<ModelGroupViewModel, ModelGroup>()
                    .ForMember(m => m.Parent, opt => opt.Ignore());
                cfg.CreateMap<ShippedProduct, ShippedProductViewModel>();
                cfg.CreateMap<ShippedProductViewModel, ShippedProduct>()
                    .ForMember(m => m.AuthorId, opt => opt.Ignore())
                    .ForMember(m => m.CreationDate, opt => opt.Ignore())
                    .ForMember(m => m.Model, opt => opt.Ignore()); ;
                cfg.CreateMap<ModelGroup, ModelGroup>()
                    .ForMember(m => m.ParentRef, opt => opt.Ignore())
                    .ForMember(m => m.Parent, opt => opt.Ignore())
                    .ForMember(m => m.AuthorId, opt => opt.Ignore())
                    .ForMember(m => m.CreationDate, opt => opt.Ignore())
                    .ForMember(m => m.Products, opt => opt.Ignore())
                    .ForMember(m => m.Childs, opt => opt.Ignore());
                cfg.CreateMap<ShippedProduct, ShippedProduct>()
                    .ForMember(m => m.AuthorId, opt => opt.Ignore())
                    .ForMember(m => m.CreationDate, opt => opt.Ignore())
                    .ForMember(m => m.Model, opt => opt.Ignore());
                cfg.CreateMap<ShippedProductViewModel, ShippedProductViewModel>();
            });
        }

        public static IMapper GetMapper()
        {
            return MapperCfg.CreateMapper();
        }
    }
}
