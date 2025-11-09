using E_Commerce.Services_Abstraction ;
using E_Commerce.Shared.DTOs.ProductDTOs ;
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
    // GET : => BaseUrl/api/Products
    public async Task < ActionResult < IEnumerable < ProductDto > > > GetAllProducts ( int ? brandId , int ? typeId )
    {
        var Products = await _productService.GetProductsAsync ( brandId , typeId ) ;
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