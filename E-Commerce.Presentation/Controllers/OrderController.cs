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
}