﻿using AutoMapper;
using StoreManagement.DTO;
using StoreManagement.DTO.AuthDTO;
using StoreManagement.Models;

namespace StoreManagement
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, AppUserDTO>().ReverseMap();
            CreateMap<AppUser, LoginDTO>().ForMember(h=>h.Email,opt => opt.MapFrom(src => src.Email))
                                          .ForMember(h => h.Password, opt => opt.MapFrom(src => src.Password)).ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Store, StoreDTO>().ReverseMap();
            CreateMap<OrderDetails, OrderDetailsDTO>().ReverseMap();
        }
    }
}
