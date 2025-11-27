using E_Commerce.Services_Abstraction ;
using E_Commerce.Shared.DTOs.BasketDTOs ;
using Microsoft.AspNetCore.Mvc ;

namespace E_Commerce.Presentation.Controllers ;

public class BasketsController : ApiBaseController
{
    private readonly IBasketService _basketService ;

    public BasketsController ( IBasketService basketService )
    {
        _basketService = basketService ;
    }

    #region Get Basket : BaseUrl/api/Baskets?id = " "

    [ HttpGet ]
    public async Task < ActionResult < BasketDto > > GetBasket ( string id )
    {
        var Basket = await _basketService.GetBasketAsync ( id ) ;
        return HandelResult < BasketDto > ( Basket ) ;
    }

    #endregion

    #region POST : BaseUrl/api/Baskets

    [ HttpPost ]
    public async Task < ActionResult < BasketDto > > CreateOrUpdateBasket ( BasketDto basketDto )
    {
        var Basket = await _basketService.CreateOrUpdateAsync ( basketDto ) ;
        return Ok ( Basket ) ;
    }

    #endregion

    #region Delete Basket : BaseUrl/api/Baskets/{id}

    [ HttpDelete ( "{id}" ) ]
    public async Task < ActionResult < bool > > DeleteBasket ( string id )
    {
        var Result = await _basketService.DeleteBasketAsync ( id ) ;
        return Ok ( Result ) ;
    }

    #endregion
}