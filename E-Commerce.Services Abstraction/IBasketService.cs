using E_Commerce.Shared.DTOs.BasketDTOs ;

namespace E_Commerce.Services_Abstraction ;

public interface IBasketService
{
    Task < BasketDto > GetBasketAsync ( string Id ) ;
    Task < BasketDto > CreateOrUpdateAsync ( BasketDto basket ) ;
    Task < bool > DeleteBasketAsync ( string id ) ;
}