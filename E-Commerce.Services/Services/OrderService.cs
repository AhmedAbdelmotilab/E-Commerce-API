using AutoMapper ;
using E_Commerce.Domain.Contracts ;
using E_Commerce.Domain.Entities.BasketModule ;
using E_Commerce.Domain.Entities.OrderModule ;
using E_Commerce.Domain.Entities.ProductModule ;
using E_Commerce.Services_Abstraction ;
using E_Commerce.Services.Specifications ;
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

    public async Task < Result < IEnumerable < OrderToReturnDto > > > GetAllOrdersAsync ( string Email )
    {
        var Specification = new OrderSpecification ( Email ) ;
        var Orders = await _unitOfWork.GetRepository < Order , Guid > ( ).GetAllAsync ( Specification ) ;
        if ( ! Orders.Any ( ) ) return Error.NotFound ( "Orders not found" , $"The Orders For${Email} Not Found" ) ;
        var Data = _mapper.Map < IEnumerable < OrderToReturnDto > > ( Orders ) ;
        return Result < IEnumerable < OrderToReturnDto > >.Ok ( Data ) ;
    }

    public async Task < Result < IEnumerable < DeliveryMethodDto > > > GetDeliveryMethodsAsync ( string Email )
    {
        var DeliveryMethods = await _unitOfWork.GetRepository < DeliveryMethod , int > ( ).GetAllAsync ( ) ;
        if ( ! DeliveryMethods.Any ( ) ) return Error.NotFound ( "DeliveryMethods not found" , $"The DM With Not Found" ) ;
        var Data = _mapper.Map < IEnumerable < DeliveryMethodDto > > ( DeliveryMethods ) ;
        return Result < IEnumerable < DeliveryMethodDto > >.Ok ( Data ) ;
    }

    public async Task < Result < OrderToReturnDto > > GetOrderByIdAsync ( Guid id , string Email )
    {
        var Specification = new OrderSpecification ( id , Email ) ;
        var Order = await _unitOfWork.GetRepository < Order , Guid > ( ).GetByIdAsync ( Specification ) ;
        if ( Order is null ) return Error.NotFound ( "Order not found" , $"The Order With ${id} Not Found" ) ;
        var Data = _mapper.Map < OrderToReturnDto > ( Order ) ;
        return Result < OrderToReturnDto >.Ok ( Data ) ;
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