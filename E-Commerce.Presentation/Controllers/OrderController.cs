using E_Commerce.Services_Abstraction ;
using E_Commerce.Shared.DTOs.OrderDTOs ;
using Microsoft.AspNetCore.Authorization ;
using Microsoft.AspNetCore.Mvc ;

namespace E_Commerce.Presentation.Controllers ;

public class OrderController : ApiBaseController
{
    private readonly IOrderService _orderService ;

    public OrderController ( IOrderService orderService )
    {
        _orderService = orderService ;
    }

    [ Authorize ]
    [ HttpPost ]
    public async Task < ActionResult < OrderToReturnDto > > CreateOrder ( OrderDto orderDto )
    {
        var Result = await _orderService.CreateOrderAsync ( orderDto , GetEmailFromToken ( ) ) ;
        return HandelResult ( Result ) ;
    }

    [ Authorize ]
    [ HttpGet ] // BaseUrl/api/Order
    public async Task < ActionResult < IEnumerable < OrderToReturnDto > > > GetOrders ( )
    {
        var Orders = await _orderService.GetAllOrdersAsync ( GetEmailFromToken ( ) ) ;
        return HandelResult ( Orders ) ;
    }

    [ Authorize ]
    [ HttpGet ( "{id:guid}" ) ] // BaseUrl/api/Order?id
    public async Task < ActionResult < OrderToReturnDto > > GetOrderById ( Guid id )
    {
        var Order = await _orderService.GetOrderByIdAsync ( id , GetEmailFromToken ( ) ) ;
        return HandelResult ( Order ) ;
    }

    [ Authorize ]
    [ HttpGet ( "DeliveryMethods" ) ] // BaseUrl/api/Order/DeliveryMethods
    public async Task < ActionResult < IEnumerable < DeliveryMethodDto > > > GetDeliveryMethods ( )
    {
        var DeliveryMethods = await _orderService.GetDeliveryMethodsAsync ( GetEmailFromToken ( ) ) ;
        return HandelResult ( DeliveryMethods ) ;
    }
}