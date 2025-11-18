using E_Commerce.Shared.CommonResult ;
using E_Commerce.Shared.DTOs.ProductDTOs ;
using E_Commerce.Shared.Pagination ;
using E_Commerce.Shared.Params ;

namespace E_Commerce.Services_Abstraction ;

public interface IProductService
{
    // 1. Get All Products 
    Task < PaginationResult < ProductDto > > GetProductsAsync ( ProductQueryParams queryParams ) ;

    // 2. Get Product By ID
    Task < Result < ProductDto > > GetProductByIdAsync ( int id ) ;

    // 3. Get All Products Brands
    Task < IEnumerable < BrandDto > > GetAllBrandsAsync ( ) ;

    // 4. Get All Products Types
    Task < IEnumerable < TypeDto > > GetAllTypesAsync ( ) ;
}