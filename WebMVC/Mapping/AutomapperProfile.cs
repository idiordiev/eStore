using AutoMapper;
using eStore.Application.DTOs;
using eStore.Domain.Entities;
using eStore.WebMVC.Models;

namespace eStore.WebMVC.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Keyboard, KeyboardViewModel>()
                .ForMember(dest => dest.SwitchName,
                    opt => opt.MapFrom(src => src.Switch == null ? "" : src.Switch.Name))
                .ForMember(dest => dest.SwitchIsClicking,
                    opt => opt.MapFrom(src => src.Switch != null && src.Switch.IsClicking))
                .ForMember(dest => dest.SwitchIsTactile,
                    opt => opt.MapFrom(src => src.Switch != null && src.Switch.IsTactile));

            CreateMap<Keyboard, GoodsViewModel>();

            CreateMap<Mouse, MouseViewModel>();

            CreateMap<Mouse, GoodsViewModel>();

            CreateMap<Mousepad, MousepadViewModel>();

            CreateMap<Mousepad, GoodsViewModel>();

            CreateMap<Gamepad, GamepadViewModel>();

            CreateMap<Gamepad, GoodsViewModel>();

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

            CreateMap<OrderItemViewModel, OrderItemDto>()
                .ForMember(dest => dest.GoodsId, opt => opt.MapFrom(src => src.GoodsId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            CreateMap<OrderViewModel, OrderAddressDto>();
        }
    }
}