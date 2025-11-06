using E_Commerce.Services_Abstraction ;
using Microsoft.AspNetCore.Mvc ;

namespace E_Commerce.Presentation.Controllers ;

[ ApiController ]
[ Route ( "api/[controller]" ) ]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService ;

    public ProductsController ( IProductService productService )
    {
        _productService = productService ;
    }
}