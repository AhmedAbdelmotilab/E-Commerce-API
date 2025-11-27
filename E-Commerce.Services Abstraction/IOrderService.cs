using E_Commerce.Shared.CommonResult ;
using E_Commerce.Shared.DTOs.OrderDTOs ;

namespace E_Commerce.Services_Abstraction ;

public interface IOrderService
{
    Task < Result < OrderToReturnDto > > CreateOrderAsync ( OrderDto orderDto , string Email ) ;
}