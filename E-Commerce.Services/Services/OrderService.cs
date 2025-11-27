using AutoMapper ;
using E_Commerce.Domain.Contracts ;
using E_Commerce.Domain.Entities.BasketModule ;
using E_Commerce.Domain.Entities.OrderModule ;
using E_Commerce.Domain.Entities.ProductModule ;
using E_Commerce.Services_Abstraction ;
using E_Commerce.Shared.CommonResult ;
using E_Commerce.Shared.DTOs.OrderDTOs ;

namespace E_Commerce.Services.Services ;

public class OrderService : IOrderService
{
    private readonly IBasketRepository _basketRepository ;
    private readonly IMapper _mapper ;
    private readonly IUnitOfWork _unitOfWork ;

    public OrderService ( IUnitOfWork unitOfWork , IMapper mapper , IBasketRepository basketRepository )
    {
        _unitOfWork = unitOfWork ;
        _mapper = mapper ;
        _basketRepository = basketRepository ;
    }

    public async Task < Result < OrderToReturnDto > > CreateOrderAsync ( OrderDto orderDto , string Email )
    {
        var OrderAddress = _mapper.Map < OrderAddress > ( orderDto.Address ) ;
        var Basket = await _basketRepository.GetBasketAsync ( orderDto.BasketId ) ;
        if ( Basket is null ) return Error.NotFound ( "Basket not found" , $"The Basket With {orderDto.BasketId} Not Found" ) ;
        List < OrderItem > OrderItems = new List < OrderItem > ( ) ;
        foreach ( var Item in Basket.Items )
        {
            var product = await _unitOfWork.GetRepository < Product , int > ( ).GetByIdAsync ( Item.Id ) ;
            if ( product is null ) return Error.NotFound ( "Product Not Found" , $"The Product With {Item.Id} Not Found" ) ;
            OrderItems.Add ( CreateOrderItem ( product , Item ) ) ;
        }

        var DeliveryMethod = await _unitOfWork.GetRepository < DeliveryMethod , int > ( ).GetByIdAsync ( orderDto.DeliveryMethodId ) ;
        if ( DeliveryMethod is null )
            return Error.NotFound ( "DeliveryMethod Not Found" , $"The DeliveryMethod With {orderDto.DeliveryMethodId} Not Found" ) ;
        var SubTotal = OrderItems.Sum ( I => I.Price * I.Quantity ) ;
        var Order = new Order ( )
        {
            Address = OrderAddress ,
            DeliveryMethod = DeliveryMethod ,
            Items = OrderItems ,
            SubTotal = SubTotal ,
            UserEmail = Email ,
        } ;
        await _unitOfWork.GetRepository < Order , Guid > ( ).AddAsync ( Order ) ;
        int Result = await _unitOfWork.SavChangesAsync ( ) ;
        if ( Result == 0 ) return Error.Failure ( "Order Failure" , "Create Order Failed" ) ;
        return _mapper.Map < OrderToReturnDto > ( Order ) ;
    }

    private static OrderItem CreateOrderItem ( Product product , BasketItem Item )
    {
        return new OrderItem ( )
        {
            Product = new ProductItemOrdered ( )
            {
                ProductId = product.Id ,
                ProductName = product.Name ,
                PictureUrl = product.PictureUrl ,
            } ,
            Price = product.Price ,
            Quantity = Item.Quantity
        } ;
    }
}