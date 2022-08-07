﻿using System.Linq;
using AutoMapper;
using eStore.ApplicationCore.Entities;
using eStore.WebMVC.Models.Goods;

namespace eStore.WebMVC.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Keyboard, KeyboardViewModel>()
                .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacturer.Name))
                .ForMember(dest => dest.SwitchName, opt => opt.MapFrom(src => src.Switch.Name))
                .ForMember(dest => dest.SwitchIsClicking, opt => opt.MapFrom(src => src.Switch.IsClicking))
                .ForMember(dest => dest.SwitchIsTactile, opt => opt.MapFrom(src => src.Switch.IsTactile))
                .ForMember(dest => dest.ConnectionType, opt => opt.MapFrom(src => src.ConnectionTypes.Select(t => t.ConnectionType.Name).Aggregate((i, j) => i + " " + j)))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.Name))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size.Name))
                .ForMember(dest => dest.KeycapMaterial, opt => opt.MapFrom(src => src.KeycapMaterial.Name))
                .ForMember(dest => dest.FrameMaterial, opt => opt.MapFrom(src => src.FrameMaterial.Name))
                .ForMember(dest => dest.KeyRollover, opt => opt.MapFrom(src => src.KeyRollover.Name))
                .ForMember(dest => dest.Backlight, opt => opt.MapFrom(src => src.Backlight.Name))
                .ReverseMap();

            CreateMap<Mouse, MouseViewModel>()
                .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacturer.Name))
                .ForMember(dest => dest.ConnectionType, opt => opt.MapFrom(src => src.ConnectionTypes.Select(t => t.ConnectionType.Name).Aggregate((i, j) => i + " " + j)))
                .ForMember(dest => dest.Backlight, opt => opt.MapFrom(src => src.Backlight.Name))
                .ReverseMap();
            
            CreateMap<Mousepad, MousepadViewModel>()
                .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacturer.Name))
                .ForMember(dest => dest.ConnectionType, opt => opt.MapFrom(src => src.ConnectionTypes.Select(t => t.ConnectionType.Name).Aggregate((i, j) => i + " " + j)))
                .ForMember(dest => dest.TopMaterial, opt => opt.MapFrom(src => src.TopMaterial.Name))
                .ForMember(dest => dest.BottomMaterial, opt => opt.MapFrom(src => src.BottomMaterial.Name))
                .ForMember(dest => dest.Backlight, opt => opt.MapFrom(src => src.Backlight.Name))
                .ReverseMap();
            
            CreateMap<Gamepad, GamepadViewModel>()
                .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacturer.Name))
                .ForMember(dest => dest.ConnectionType, opt => opt.MapFrom(src => src.ConnectionTypes.Select(t => t.ConnectionType.Name).Aggregate((i, j) => i + " " + j)))
                .ForMember(dest => dest.Feedback, opt => opt.MapFrom(src => src.Feedback.Name))
                .ReverseMap();

        }
    }
}