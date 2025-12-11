using E_Commerce.Services_Abstraction ;
using E_Commerce.Shared.DTOs.BasketDTOs ;
using Microsoft.AspNetCore.Mvc ;

namespace E_Commerce.Presentation.Controllers ;

public class PaymentController : ApiBaseController
{
    private readonly IPaymentService _paymentService ;

    public PaymentController ( IPaymentService paymentService )
    {
        _paymentService = paymentService ;
    }

    [ HttpPost ( "{basketId}" ) ]
    public async Task < ActionResult < BasketDto > > CreateOrUpdate ( string basketId )
    {
        var Basket = await _paymentService.CreateOrUpdatePaymentAsync ( basketId ) ;
        return Ok ( Basket ) ;
    }

    [ HttpPost ( "WebHook" ) ]
    public async Task < IActionResult > WebHook ( )
    {
        var json = await new StreamReader ( HttpContext.Request.Body ).ReadToEndAsync ( ) ;
        await _paymentService.UpdatePaymentStatusAsync ( json ,
            Request.Headers [ "Stripe-Signature" ]! ) ;
        return new EmptyResult ( ) ;
    }
}