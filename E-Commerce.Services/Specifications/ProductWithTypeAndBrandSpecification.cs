using E_Commerce.Domain.Entities.ProductModule ;
using E_Commerce.Shared.Params ;

namespace E_Commerce.Services.Specifications ;

public class ProductWithTypeAndBrandSpecification : BaseSpecification < Product , int >
{
    public ProductWithTypeAndBrandSpecification ( ProductQueryParams queryParams )
        : base ( P => ( ! queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId.Value )
                      && ( ! queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId.Value )
                      && ( string.IsNullOrEmpty ( queryParams.Search ) || P.Name.ToLower ( ).Contains ( queryParams.Search.ToLower ( ) ) ) )
    {
        AddInclude ( P => P.ProductType ) ;
        AddInclude ( P => P.ProductBrand ) ;
    }

    public ProductWithTypeAndBrandSpecification ( int id ) : base ( P => P.Id == id )
    {
        AddInclude ( P => P.ProductType ) ;
        AddInclude ( P => P.ProductBrand ) ;
    }
}