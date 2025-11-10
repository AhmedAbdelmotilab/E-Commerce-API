using E_Commerce.Domain.Entities.ProductModule ;
using E_Commerce.Shared.Params ;

namespace E_Commerce.Services.Specifications ;

public class ProductCountSpecification : BaseSpecification < Product , int >
{
    public ProductCountSpecification ( ProductQueryParams queryParams )
        : base ( P => ( ! queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId.Value )
                      && ( ! queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId.Value )
                      && ( string.IsNullOrEmpty ( queryParams.Search ) ||
                           P.Name.ToLower ( ).Contains ( queryParams.Search.ToLower ( ) ) ) ) { }
}