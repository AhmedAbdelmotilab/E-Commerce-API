using E_Commerce.Domain.Entities.ProductModule ;
using E_Commerce.Shared.Params ;

namespace E_Commerce.Services.Specifications ;

public class ProductCountSpecification : BaseSpecification < Product , int >
{
    public ProductCountSpecification ( ProductQueryParams queryParams ) : base (
        ProductSpecificationHelper.GetProductCriteria ( queryParams ) ) { }
}