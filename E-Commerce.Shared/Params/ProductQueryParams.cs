using E_Commerce.Shared.Enums ;

namespace E_Commerce.Shared.Params ;

public class ProductQueryParams
{
    public int ? TypeId { get ; set ; }
    public int ? BrandId { get ; set ; }
    public string ? Search { get ; set ; }
    public ProductSortingOptions ? Sort { get ; set ; }
}