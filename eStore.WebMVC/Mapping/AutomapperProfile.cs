using System.Linq;
using AutoMapper;
using eStore.ApplicationCore.Entities;
using eStore.WebMVC.DTO;
using eStore.WebMVC.Models;
using eStore.WebMVC.Models.Goods;

namespace eStore.WebMVC.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Keyboard, KeyboardViewModel>()
                .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacturer.Name))
                .ForMember(dest => dest.SwitchName, opt => opt.MapFrom(src => src.Switch == null ? "" : src.Switch.Name))
                .ForMember(dest => dest.SwitchIsClicking, opt => opt.MapFrom(src => src.Switch != null && src.Switch.IsClicking))
                .ForMember(dest => dest.SwitchIsTactile, opt => opt.MapFrom(src => src.Switch != null && src.Switch.IsTactile))
                .ForMember(dest => dest.ConnectionType, opt => opt.MapFrom(src => src.ConnectionType.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.Name))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size.Name))
                .ForMember(dest => dest.KeycapMaterial, opt => opt.MapFrom(src => src.KeycapMaterial.Name))
                .ForMember(dest => dest.FrameMaterial, opt => opt.MapFrom(src => src.FrameMaterial.Name))
                .ForMember(dest => dest.KeyRollover, opt => opt.MapFrom(src => src.KeyRollover.Name))
                .ForMember(dest => dest.Backlight, opt => opt.MapFrom(src => src.Backlight.Name));

            CreateMap<Keyboard, GoodsViewModel>()
                .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacturer.Name))
                .ForMember(dest => dest.ConnectionType, opt => opt.MapFrom(src => src.ConnectionType.Name));

            CreateMap<Mouse, MouseViewModel>()
                .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacturer.Name))
                .ForMember(dest => dest.ConnectionType, opt => opt.MapFrom(src => src.ConnectionType.Name))
                .ForMember(dest => dest.Backlight, opt => opt.MapFrom(src => src.Backlight.Name));
            
            CreateMap<Mouse, GoodsViewModel>()
                .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacturer.Name))
                .ForMember(dest => dest.ConnectionType, opt => opt.MapFrom(src => src.ConnectionType.Name));

            CreateMap<Mousepad, MousepadViewModel>()
                .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacturer.Name))
                .ForMember(dest => dest.ConnectionType, opt => opt.MapFrom(src => src.ConnectionType.Name))                
                .ForMember(dest => dest.TopMaterial, opt => opt.MapFrom(src => src.TopMaterial.Name))
                .ForMember(dest => dest.BottomMaterial, opt => opt.MapFrom(src => src.BottomMaterial.Name))
                .ForMember(dest => dest.Backlight, opt => opt.MapFrom(src => src.Backlight.Name));
            
            CreateMap<Mousepad, GoodsViewModel>()
                .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacturer.Name))
                .ForMember(dest => dest.ConnectionType, opt => opt.MapFrom(src => src.ConnectionType.Name));

            CreateMap<Gamepad, GamepadViewModel>()
                .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacturer.Name))
                .ForMember(dest => dest.ConnectionType, opt => opt.MapFrom(src => src.ConnectionType.Name))
                .ForMember(dest => dest.Feedback, opt => opt.MapFrom(src => src.Feedback.Name));

            CreateMap<Gamepad, GoodsViewModel>()
                .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacturer.Name))
                .ForMember(dest => dest.ConnectionType, opt => opt.MapFrom(src => src.ConnectionType.Name));

            CreateMap<Customer, CustomerViewModel>()
                .ForMember(dest => dest.GoodsInCart, opt => opt.Ignore());

            CreateMap<CustomerViewModel, Customer>()
                .ForMember(dest => dest.Orders, opt => opt.Ignore())
                .ForMember(dest => dest.ShoppingCart, opt => opt.Ignore());
            
            CreateMap<OrderItem, OrderItemViewModel>()
                .ForMember(dest => dest.Goods, opt => opt.MapFrom(src => src.Goods));
            
            CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<OrderItemViewModel, OrderItemDTO>()
                .ForMember(dest => dest.GoodsId, opt => opt.MapFrom(src => src.GoodsId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            CreateMap<OrderViewModel, OrderAddressDTO>();
        }
    }
}