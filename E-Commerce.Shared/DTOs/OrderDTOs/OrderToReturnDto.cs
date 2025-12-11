namespace E_Commerce.Shared.DTOs.OrderDTOs ;

public record OrderToReturnDto
{
    public Guid Id { get ; init ; }
    public string UserEmail { get ; init ; } = default! ;
    public ICollection < OrderItemDto > Items { get ; init ; } = [ ] ;
    public AddressDto Address { get ; init ; } = default! ;
    public string DeliveryMethod { get ; init ; } = default! ;
    public string OrderStatus { get ; init ; } = default! ;
    public DateTimeOffset OrderDate { get ; init ; }
    public decimal SubTotal { get ; init ; }
    public decimal Total { get ; init ; }
}