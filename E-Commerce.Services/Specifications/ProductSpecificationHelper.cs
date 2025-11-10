using System.Linq.Expressions ;
using E_Commerce.Domain.Entities.ProductModule ;
using E_Commerce.Shared.Params ;

namespace E_Commerce.Services.Specifications ;

public static class ProductSpecificationHelper
{
    public static Expression < Func < Product , bool > > GetProductCriteria ( ProductQueryParams queryParams )
    {
        return P => ( ! queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId.Value )
                    && ( ! queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId.Value )
                    && ( string.IsNullOrEmpty ( queryParams.Search ) ||
                         P.Name.ToLower ( ).Contains ( queryParams.Search.ToLower ( ) ) ) ;
    }
}