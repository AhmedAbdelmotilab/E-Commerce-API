using E_Commerce.Domain.Entities.SharedModule ;

namespace E_Commerce.Domain.Entities.ProductModule ;

public class ProductBrand : BaseEntity < int >
{
    public string Name { get ; set ; } = default! ;
}