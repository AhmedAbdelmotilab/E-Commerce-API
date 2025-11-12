using E_Commerce.Services_Abstraction ;
using Microsoft.AspNetCore.Mvc ;

namespace E_Commerce.Presentation.Controllers ;

[ ApiController ]
[ Route ( "api/[controller]" ) ]
public class BasketsController : ControllerBase
{
    private readonly IBasketService _basketService ;

    public BasketsController ( IBasketService basketService )
    {
        _basketService = basketService ;
    }
}