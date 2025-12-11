using AutoMapper ;
using E_Commerce.Domain.Entities.OrderModule ;
using E_Commerce.Shared.DTOs.OrderDTOs ;
using Microsoft.Extensions.Configuration ;

namespace E_Commerce.Services.MappingProfile ;

public class OrderItemPictureUrlResolver : IValueResolver < OrderItem , OrderItemDto , string >
{
    private readonly IConfiguration _configuration ;

    public OrderItemPictureUrlResolver ( IConfiguration configuration )
    {
        _configuration = configuration ;
    }

    public string Resolve ( OrderItem source , OrderItemDto destination , string destMember , ResolutionContext context )
    {
        if ( string.IsNullOrEmpty ( source.Product.PictureUrl ) ) return string.Empty ;
        if ( source.Product.PictureUrl.StartsWith ( "http" ) ) return source.Product.PictureUrl ;
        var BaseUrl = _configuration.GetSection ( "URLs" ) [ "BaseUrl" ] ;
        if ( string.IsNullOrEmpty ( BaseUrl ) ) return string.Empty ;
        var picUrl = $"{BaseUrl}{source.Product.PictureUrl}" ;
        return picUrl ;
    }
}