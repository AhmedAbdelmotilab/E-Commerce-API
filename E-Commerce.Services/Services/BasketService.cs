using AutoMapper ;
using E_Commerce.Domain.Contracts ;
using E_Commerce.Domain.Entities.BasketModule ;
using E_Commerce.Services_Abstraction ;
using E_Commerce.Services.Exceptions ;
using E_Commerce.Shared.CommonResult ;
using E_Commerce.Shared.DTOs.BasketDTOs ;

namespace E_Commerce.Services.Services ;

public class BasketService : IBasketService
{
    private readonly IMapper _mapper ;
    private readonly IBasketRepository _repository ;

    public BasketService ( IBasketRepository repository , IMapper mapper )
    {
        _repository = repository ;
        _mapper = mapper ;
    }

    public async Task < Result<BasketDto> > GetBasketAsync ( string Id )
    {
        var Basket = await _repository.GetBasketAsync ( Id ) ;
        if ( Basket is null )
        {
            return Error.NotFound ( "Basket not found" , $"Basket With {Id} Is Not Found" ) ;
        }

        return _mapper.Map < CustomerBasket , BasketDto > ( Basket! ) ;
    }

    public async Task < BasketDto > CreateOrUpdateAsync ( BasketDto basket )
    {
        var CustomerBasket = _mapper.Map < CustomerBasket > ( basket ) ;
        var CreatedOrUpdatedBasket = await _repository.CreateOrUpdateAsync ( CustomerBasket ) ;
        return _mapper.Map < CustomerBasket , BasketDto > ( CreatedOrUpdatedBasket ! ) ;
    }

    public Task < bool > DeleteBasketAsync ( string id ) => _repository.DeleteBasketAsync ( id ) ;
}