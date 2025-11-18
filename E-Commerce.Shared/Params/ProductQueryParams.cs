using E_Commerce.Shared.Enums ;

namespace E_Commerce.Shared.Params ;

public class ProductQueryParams
{
    private const int DefaultPageSize = 5 ;
    private const int MaximumPageSize = 10 ;
    private int _pageIndex = 1 ;
    private int _pageSize = DefaultPageSize ;
    public int ? TypeId { get ; set ; }
    public int ? BrandId { get ; set ; }
    public string ? Search { get ; set ; }
    public ProductSortingOptions ? Sort { get ; set ; }

    public int PageIndex { get { return _pageIndex ; } set { _pageIndex = ( value <= 0 ) ? 1 : value ; } }

    public int PageSize
    {
        get { return _pageSize ; }
        set
        {
            if ( value <= 0 )
            {
                _pageSize = DefaultPageSize ;
            }

            if ( value > MaximumPageSize )
            {
                _pageSize = MaximumPageSize ;
            }
            else
            {
                _pageSize = value ;
            }
        }
    }
}