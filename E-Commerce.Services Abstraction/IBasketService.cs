using E_Commerce.Shared.CommonResult ;
using E_Commerce.Shared.DTOs.BasketDTOs ;

namespace E_Commerce.Services_Abstraction ;

public interface IBasketService
{
    Task < Result < BasketDto > > GetBasketAsync ( string Id ) ;
    Task < BasketDto > CreateOrUpdateAsync ( BasketDto basket ) ;
    Task < bool > DeleteBasketAsync ( string id ) ;
}