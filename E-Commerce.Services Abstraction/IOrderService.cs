using E_Commerce.Shared.CommonResult ;
using E_Commerce.Shared.DTOs.OrderDTOs ;

namespace E_Commerce.Services_Abstraction ;

public interface IOrderService
{
    Task < Result < OrderToReturnDto > > CreateOrderAsync ( OrderDto orderDto , string Email ) ;
    Task < Result < IEnumerable < OrderToReturnDto > > > GetAllOrdersAsync ( string Email ) ;
    Task < Result < IEnumerable < DeliveryMethodDto > > > GetDeliveryMethodsAsync ( string Email ) ;
    Task < Result < OrderToReturnDto > > GetOrderByIdAsync ( Guid id , string Email ) ;
}