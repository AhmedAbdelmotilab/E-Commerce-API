using System.Text.Json ;
using E_Commerce.Domain.Contracts ;
using E_Commerce.Services_Abstraction ;

namespace E_Commerce.Services.Services ;

public class CacheService : ICacheService
{
    private readonly ICacheRepository _cacheRepository ;

    public CacheService ( ICacheRepository cacheRepository )
    {
        _cacheRepository = cacheRepository ;
    }

    public async Task < string ? > GetAsync ( string CacheKey )
    {
        return await _cacheRepository.GetAsync ( CacheKey ) ;
    }

    public Task SetAsync ( string CacheKey , object CacheValue , TimeSpan TimeToLive )
    {
        var Value = JsonSerializer.Serialize ( CacheValue ,
            new JsonSerializerOptions ( )
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            } ) ;
        return _cacheRepository.SetAsync ( CacheKey , Value , TimeToLive ) ;
    }
}