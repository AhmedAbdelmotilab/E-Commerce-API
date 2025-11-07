using AutoMapper ;
using E_Commerce.Domain.Entities.ProductModule ;
using E_Commerce.Shared.DTOs.ProductDTOs ;

namespace E_Commerce.Services.MappingProfile ;

public class ProductProfile : Profile
{
    public ProductProfile ( )
    {
        #region Product Mapping

        // 1. Map The Entity + Mapping The Navigational Proprieties Of Product Brand And Type
        // 2. We Use The Resolver OverLoad For Getting The Image URL
        //  Is Like This => {{BaseURL}}/api/Products/images/products/ClassicWhiteTShirt.jpeg
        CreateMap < Product , ProductDto > ( )
            .ForMember ( dest => dest.ProductType , opt => opt.MapFrom ( src => src.ProductType.Name ) )
            .ForMember ( dest => dest.ProductBrand , opt => opt.MapFrom ( src => src.ProductBrand.Name ) )
            .ForMember ( des => des.PictureUrl , opt => opt.MapFrom < ProductPictureUrlResolver > ( ) ) ;

        #endregion

        #region ProductBrand Mapping

        CreateMap < ProductBrand , BrandDto > ( ) ;

        #endregion

        #region ProductType Mapping

        CreateMap < ProductType , TypeDto > ( ) ;

        #endregion
    }
}