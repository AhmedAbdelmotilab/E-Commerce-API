using E_Commerce.Presentation.Attributes ;
using E_Commerce.Services_Abstraction ;
using E_Commerce.Shared.DTOs.ProductDTOs ;
using E_Commerce.Shared.Pagination ;
using E_Commerce.Shared.Params ;
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

    #region GET ALL PRODUCTS

    [ HttpGet ]
    [ RedisCache ( 10 ) ]
    // GET : => BaseUrl/api/Products
    public async Task < ActionResult < PaginationResult < ProductDto > > >
        GetAllProducts ( [ FromQuery ] ProductQueryParams queryParams )
    {
        var Products = await _productService.GetProductsAsync ( queryParams ) ;
        return Ok ( Products ) ;
    }

    #endregion

    #region GET PRODUCT BY ID

    [ HttpGet ( "{id}" ) ]
    // GET : => BaseUrl/api/Products/1
    public async Task < ActionResult < ProductDto > > GetProductById ( int id )
    {
        var Product = await _productService.GetProductByIdAsync ( id ) ;
        return Ok ( Product ) ;
    }

    #endregion

    #region GET PRODUCT BRANDS

    // GET : => BaseUrl/api/Products/brands
    [ HttpGet ( "brands" ) ]
    public async Task < ActionResult < IEnumerable < BrandDto > > > GetProductBrands ( )
    {
        var Brands = await _productService.GetAllBrandsAsync ( ) ;
        return Ok ( Brands ) ;
    }

    #endregion

    #region GET PRODUCT TYPES

    [ HttpGet ( "types" ) ]
    // GET : => BaseUrl/api/Products/types
    public async Task < ActionResult < IEnumerable < TypeDto > > > GetProductTypes ( )
    {
        var Types = await _productService.GetAllTypesAsync ( ) ;
        return Ok ( Types ) ;
    }

    #endregion
}