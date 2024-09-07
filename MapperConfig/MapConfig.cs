using AutoMapper;
using CarRentals.Entities;
using CarRentals.DTOs.BookingDto;
using CarRentals.DTOs.CarDto;
using CarRentals.DTOs.CategoryDto;
using CarRentals.DTOs.CommentDto;
using CarRentals.DTOs.RoleDto;
using CarRentals.Models;

namespace Carentals.MapperConfig;

public class MapConfig : Profile
{
    public MapConfig()
    {
        //Boooking mapping config
        CreateMap<BookingListDto, Booking>()
            .ForMember(dest =>dest.Car.Name,opt=>opt.MapFrom(src=>src.CarName)) 
            .ForMember(dest =>$"{ dest.User.FirstName} { dest.User.LastName} ",opt=>opt.MapFrom(src=>src.UserName))
            .ReverseMap();
        CreateMap<BookingDetailDto, Booking>()
             .ForMember(dest => dest.Car.Name, opt => opt.MapFrom(src => src.CarName))
            .ForMember(dest => $"{dest.User.FirstName} {dest.User.LastName} ", opt => opt.MapFrom(src => src.UserName))
            .ReverseMap();
        CreateMap<BookingCreateDto, Booking>().ReverseMap();


        //Car Mapping Config
        CreateMap<CarCreateDto, Car>().ForMember(dest => dest.CarGalleries, opt => opt.MapFrom(src => new CarGalleryModel()
        {
            
        })).ReverseMap();
        CreateMap<CarDetailDto, Car>().ReverseMap();
        CreateMap<CarListDto, Car>().ReverseMap();

        //Category Mapping Config
        CreateMap<CategoryListDto, Category>().ReverseMap();
        CreateMap<CategoryDetailDto, Category>().ReverseMap();
        CreateMap<CategoryCreateDto, Category>().ReverseMap();
        CreateMap<CategoryUpdateDto, Category>().ReverseMap();

        //Comment Mapping Config
        CreateMap<CommentDetailDto, Comment>().ReverseMap();
        CreateMap<CommentListDto, Comment>().ReverseMap();
        CreateMap<CommentCreateDto, Comment>().ReverseMap();
        CreateMap<CommentUpdateDto, Comment>().ReverseMap();

        //Role Mapping Config
        CreateMap<RoleCreateDto, Role>().ReverseMap();
        CreateMap<RoleListDto, Role>().ReverseMap();
        CreateMap<RoleUpdateDto, Role>().ReverseMap();
        CreateMap<RoleUpdateDto, Role>().ReverseMap();

    }
}

