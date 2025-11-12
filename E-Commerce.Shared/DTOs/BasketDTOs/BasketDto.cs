namespace E_Commerce.Shared.DTOs.BasketDTOs ;

public record BasketDto ( string Id , ICollection < BasketItemDto > Items ) ;