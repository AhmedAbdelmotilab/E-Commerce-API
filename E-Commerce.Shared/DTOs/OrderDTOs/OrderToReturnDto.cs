namespace E_Commerce.Shared.DTOs.OrderDTOs ;

public record OrderToReturnDto
(
    Guid Id ,
    string UserEmail ,
    ICollection < OrderItemDto > Items ,
    AddressDto Address ,
    string DeliveryMethod ,
    string OrderStatus ,
    DateTimeOffset OrderDate ,
    decimal SubTotal ,
    decimal Total
) ;