using E_Commerce.Domain.Entities.ProductModule ;

namespace E_Commerce.Services.Specifications ;

public class ProductWithTypeAndBrandSpecification : BaseSpecification < Product , int >
{
    public ProductWithTypeAndBrandSpecification ( int ? brandId , int ? typeId )
        : base ( P => ( ! brandId.HasValue || P.BrandId == brandId.Value ) && ( ! typeId.HasValue || P.TypeId == typeId.Value ) )
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