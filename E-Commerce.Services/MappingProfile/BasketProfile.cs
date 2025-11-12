using AutoMapper ;
using E_Commerce.Domain.Entities.BasketModule ;
using E_Commerce.Shared.DTOs.BasketDTOs ;

namespace E_Commerce.Services.MappingProfile ;

public class BasketProfile : Profile
{
    public BasketProfile ( )
    {
        CreateMap < CustomerBasket , BasketDto > ( ).ReverseMap ( ) ;
        CreateMap < BasketItem , BasketItemDto > ( ).ReverseMap ( ) ;
    }
}