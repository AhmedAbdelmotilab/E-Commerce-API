using AutoMapper ;
using E_Commerce.Domain.Entities.ProductModule ;
using E_Commerce.Shared.DTOs.ProductDTOs ;
using Microsoft.Extensions.Configuration ;

namespace E_Commerce.Services.MappingProfile ;

public class ProductPictureUrlResolver : IValueResolver < Product , ProductDto , string >
{
    private readonly IConfiguration _configuration ;

    public ProductPictureUrlResolver ( IConfiguration configuration )
    {
        _configuration = configuration ;
    }

    public string Resolve ( Product source , ProductDto destination , string destMember , ResolutionContext context )
    {
        if ( string.IsNullOrEmpty ( source.PictureUrl ) ) return string.Empty ;
        if ( source.PictureUrl.StartsWith ( "http" ) ) return source.PictureUrl ;
        var BaseUrl = _configuration.GetSection ( "URLs" ) [ "BaseUrl" ] ;
        if ( string.IsNullOrEmpty ( BaseUrl ) ) return string.Empty ;
        var picUrl = $"{BaseUrl}{source.PictureUrl}" ;
        return picUrl ;
    }
}