using E_Commerce.Domain.Entities.ProductModule ;
using E_Commerce.Shared.Enums ;
using E_Commerce.Shared.Params ;

namespace E_Commerce.Services.Specifications ;

public class ProductWithTypeAndBrandSpecification : BaseSpecification < Product , int >
{
    public ProductWithTypeAndBrandSpecification ( ProductQueryParams queryParams )
        : base ( P => ( ! queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId.Value )
                      && ( ! queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId.Value )
                      && ( string.IsNullOrEmpty ( queryParams.Search ) ||
                           P.Name.ToLower ( ).Contains ( queryParams.Search.ToLower ( ) ) ) )
    {
        AddInclude ( P => P.ProductType ) ;
        AddInclude ( P => P.ProductBrand ) ;
        switch ( queryParams.Sort )
        {
            case ProductSortingOptions.NameAsc :
                AddOrderBy ( P => P.Name ) ;
                break ;
            case ProductSortingOptions.NameDesc :
                AddOrderByDescending ( P => P.Name ) ;
                break ;
            case ProductSortingOptions.PriceAsc :
                AddOrderBy ( P => P.Price ) ;
                break ;
            case ProductSortingOptions.PriceDesc :
                AddOrderByDescending ( P => P.Price ) ;
                break ;
            default :
                AddOrderBy ( X => X.Id ) ;
                break ;
        }

        ApplyPagination ( queryParams.PageSize , queryParams.PageIndex ) ;
    }

    public ProductWithTypeAndBrandSpecification ( int id ) : base ( P => P.Id == id )
    {
        AddInclude ( P => P.ProductType ) ;
        AddInclude ( P => P.ProductBrand ) ;
    }
}