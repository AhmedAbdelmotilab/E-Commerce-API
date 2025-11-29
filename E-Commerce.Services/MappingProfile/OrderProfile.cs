using AutoMapper ;
using E_Commerce.Domain.Entities.OrderModule ;
using E_Commerce.Shared.DTOs.OrderDTOs ;

namespace E_Commerce.Services.MappingProfile ;

public class OrderProfile : Profile
{
    public OrderProfile ( )
    {
        CreateMap < AddressDto , OrderAddress > ( ).ReverseMap ( ) ;
        CreateMap < Order , OrderToReturnDto > ( )
            .ForMember ( D => D.DeliveryMethod , O => O.MapFrom ( src => src.DeliveryMethod.ShortName ) ) ;
        CreateMap < OrderItem , OrderItemDto > ( )
            .ForMember ( D => D.ProductName , O => O.MapFrom ( src => src.Product.ProductName ) )
            .ForMember ( D => D.PictureUrl , O => O.MapFrom < OrderItemPictureUrlResolver > ( ) ) ;
    }
}