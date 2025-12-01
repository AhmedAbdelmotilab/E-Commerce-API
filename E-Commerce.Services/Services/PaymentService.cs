using AutoMapper ;
using E_Commerce.Domain.Contracts ;
using E_Commerce.Domain.Entities.OrderModule ;
using E_Commerce.Services_Abstraction ;
using E_Commerce.Services.Exceptions ;
using E_Commerce.Services.Specifications ;
using E_Commerce.Shared.DTOs.BasketDTOs ;
using Microsoft.Extensions.Configuration ;
using Stripe ;
using Product = E_Commerce.Domain.Entities.ProductModule.Product ;

namespace E_Commerce.Services.Services ;

public class PaymentService : IPaymentService
{
    private readonly IBasketRepository _basketRepository ;
    private readonly IConfiguration _configuration ;
    private readonly IMapper _mapper ;
    private readonly IUnitOfWork _unitOfWork ;

    public PaymentService (
        IConfiguration configuration , IBasketRepository basketRepository , IUnitOfWork unitOfWork , IMapper mapper )
    {
        _configuration = configuration ;
        _basketRepository = basketRepository ;
        _unitOfWork = unitOfWork ;
        _mapper = mapper ;
    }

    public async Task < BasketDto > CreateOrUpdatePaymentAsync ( string basketId )
    {
        StripeConfiguration.ApiKey = _configuration.GetSection ( "Stripe" ) [ "SecretKey" ] ;
        var Basket = await _basketRepository.GetBasketAsync ( basketId ) ;
        if ( Basket is null ) throw new BasketNotFound ( basketId ) ;
        foreach ( var item in Basket.Items )
        {
            var Product = await _unitOfWork.GetRepository < Product , int > ( ).GetByIdAsync ( item.Id ) ;
            if ( Product is null ) throw new ProductNotFound ( item.Id ) ;
            item.Price = Product.Price ;
        }

        if ( Basket.DeliveryMethodId is null ) throw new ArgumentNullException ( ) ;
        var DeliveryMethod =
            await _unitOfWork.GetRepository < DeliveryMethod , int > ( ).GetByIdAsync ( Basket.DeliveryMethodId.Value ) ;
        if ( DeliveryMethod is null ) throw new Exception ( "DeliveryMethod Not Found" ) ;
        Basket.ShippingPrice = DeliveryMethod.Price ;
        var Amount = ( long ) ( Basket.Items.Sum ( I => I.Price * I.Quantity ) + Basket.ShippingPrice ) * 100 ;
        var Service = new PaymentIntentService ( ) ;
        if ( string.IsNullOrEmpty ( Basket.PaymentIntentId ) )
        {
            var options = new PaymentIntentCreateOptions ( )
            {
                Amount = Amount ,
                Currency = "AED" ,
                PaymentMethod = "card"
            } ;
            var PaymentIndent = await Service.CreateAsync ( options ) ;
            Basket.PaymentIntentId = PaymentIndent.Id ;
            Basket.ClientSecret = PaymentIndent.ClientSecret ;
        }
        else
        {
            var options = new PaymentIntentUpdateOptions ( )
            {
                Amount = Amount
            } ;
            await Service.UpdateAsync ( Basket.PaymentIntentId , options ) ;
        }

        await _basketRepository.CreateOrUpdateAsync ( Basket ) ;
        return _mapper.Map < BasketDto > ( Basket ) ;
    }

    public async Task UpdatePaymentStatusAsync ( string request , string Header )
    {
        var EndpointSecret = _configuration.GetRequiredSection ( "Stripe" ) [ "EndPointSecret" ] ;
        var StripeEvent = EventUtility.ConstructEvent ( request , Header , EndpointSecret ) ;
        var paymentIntent = StripeEvent.Data.Object as PaymentIntent ;
        switch ( StripeEvent.Type )
        {
            case EventTypes.PaymentIntentPaymentFailed :
                await UpdatePaymentFailedAsync ( paymentIntent!.Id ) ;
                break ;
            case EventTypes.PaymentIntentSucceeded :
                await UpdatePaymentReceivedAsync ( paymentIntent!.Id ) ;
                break ;
            default :
                Console.WriteLine ( "Unhandled event type: {0}" , StripeEvent.Type ) ;
                break ;
        }
    }

    private async Task UpdatePaymentReceivedAsync ( string paymentIntentId )
    {
        var order = await _unitOfWork.GetRepository < Order , Guid > ( )
            .GetByIdAsync ( new OrderWithPaymentIntentSpecification ( paymentIntentId ) ) ;
        if ( order is null ) throw new NullReferenceException ( "Order Not Found" ) ;
        order.OrderStatus = OrderStatus.PaymentReceived ;
        _unitOfWork.GetRepository < Order , Guid > ( ).Update ( order ) ;
        await _unitOfWork.SavChangesAsync ( ) ;
    }

    private async Task UpdatePaymentFailedAsync ( string paymentIntentId )
    {
        var order = await _unitOfWork.GetRepository < Order , Guid > ( )
            .GetByIdAsync ( new OrderWithPaymentIntentSpecification ( paymentIntentId ) ) ;
        if ( order is null ) throw new NullReferenceException ( "Order Not Found" ) ;
        order.OrderStatus = OrderStatus.PaymentFailed ;
        _unitOfWork.GetRepository < Order , Guid > ( ).Update ( order ) ;
        await _unitOfWork.SavChangesAsync ( ) ;
    }
}