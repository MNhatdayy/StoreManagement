using AutoMapper;
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
<<<<<<< HEAD
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Store, StoreDTO>().ReverseMap();
            CreateMap<OrderDetails, OrderDetailsDTO>().ReverseMap();
=======
            
            CreateMap<Table, TableDTO>().ReverseMap();
            CreateMap<Menu, MenuDTO>().ReverseMap();
            CreateMap<FoodCategory, FoodCategoryDTO>().ReverseMap();
            CreateMap<FoodItem, FoodItemDTO>().ReverseMap();
            CreateMap<MenuDetail, MenuDetailDTO>().ReverseMap();
>>>>>>> ad7998dbcf9db7311fa075da5d8a1ef96d6a9d86
        }
    }
}
