using System.Text.Json ;
using E_Commerce.Domain.Contracts ;
using E_Commerce.Domain.Entities.BasketModule ;
using StackExchange.Redis ;

namespace E_Commerce.Persistence.Repositories ;

public class BasketRepository : IBasketRepository
{
    private readonly IDatabase _database ;

    public BasketRepository ( IConnectionMultiplexer connection )
    {
        _database = connection.GetDatabase ( ) ;
    }

    public async Task < CustomerBasket ? > CreateOrUpdateAsync ( CustomerBasket basket , TimeSpan timeToLive = default )
    {
        var JsonBasket = JsonSerializer.Serialize ( basket ) ;
        var IsCreatedOrUpdated = await
            _database.StringSetAsync ( basket.Id , JsonBasket , ( timeToLive == default ) ? TimeSpan.FromDays ( 7 ) : timeToLive ) ;
        if ( IsCreatedOrUpdated )
        {
            return await GetBasketAsync ( basket.Id ) ;
        }
        else
        {
            return null ;
        }
    }

    public async Task < CustomerBasket ? > GetBasketAsync ( string basketId )
    {
        var Basket = await _database.StringGetAsync ( basketId ) ;
        if ( Basket.IsNullOrEmpty ) return null ;
        return JsonSerializer.Deserialize < CustomerBasket > ( Basket! ) ;
    }

    public async Task < bool > DeleteBasketAsync ( string basketId ) => await _database.KeyDeleteAsync ( basketId ) ;
}