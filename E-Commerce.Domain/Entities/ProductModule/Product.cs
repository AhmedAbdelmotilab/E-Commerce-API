using E_Commerce.Domain.Entities.SharedModule ;

namespace E_Commerce.Domain.Entities.ProductModule ;

public class Product : BaseEntity < int >
{
    public string Name { get ; set ; } = default! ;
    public string Description { get ; set ; } = default! ;
    public string PictureUrl { get ; set ; } = default! ;
    public decimal Price { get ; set ; }

    #region Relationships

    #region Relationship Product M >-----> 1 ProductBrand

    public int BrandId { get ; set ; }
    public ProductBrand ProductBrand { get ; set ; } = default! ;

    #endregion

    #region Relationship Product M >-----> 1 ProductType

    public int TypeId { get ; set ; }
    public ProductType ProductType { get ; set ; } = default! ;

    #endregion

    #endregion
}