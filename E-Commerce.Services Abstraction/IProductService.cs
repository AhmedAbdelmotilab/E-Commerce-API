using E_Commerce.Shared.DTOs.ProductDTOs ;

namespace E_Commerce.Services_Abstraction ;

public interface IProductService
{
    // 1. Get All Products 
    Task < IEnumerable < ProductDto > > GetProductsAsync ( ) ;

    // 2. Get Product By ID
    Task < ProductDto > GetProductByIdAsync ( int id ) ;

    // 3. Get All Products Brands
    Task < IEnumerable < BrandDto > > GetAllBrandsAsync ( ) ;

    // 4. Get All Products Types
    Task < IEnumerable < TypeDto > > GetAllBrandByIdAsync ( ) ;
}